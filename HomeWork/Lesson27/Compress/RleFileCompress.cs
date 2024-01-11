using Lesson4.Arrays;

namespace Lesson27.Compress
{
    /// <summary>
    /// Улучшенный алгоритм RLE.
    /// </summary>
    internal class RleFileCompress : BaseRleFileCompress
    {
        /// <summary>
        /// Улучшенный алгоритм сжатия RLE
        /// </summary>
        /// <param name="streamReader"></param>
        /// <param name="streamWriter"></param>
        /// <returns></returns>
        protected override async Task CompressInternal(FileStream streamReader, FileStream streamWriter)
        {
            var length = 0;
            var readBuffer = new byte[1];
            if (await streamReader.ReadAsync(readBuffer, 0, 1) < 0) return;
            var prevByte = readBuffer[0];
            var uniqueBytes = new VectorArray<byte>(100, 100);
            var readCount = 1;
            while (await streamReader.ReadAsync(readBuffer, 0, 1) > 0)
            {
                var readByte = readBuffer[0];
                readCount++;
                // считываем неповторяющиеся байты
                while (readByte != prevByte)
                {
                    readCount++;
                    uniqueBytes.Add(prevByte);
                    var readCnt = await streamReader.ReadAsync(readBuffer, 0, 1);
                    if (readCnt <= 0)
                    {
                        uniqueBytes.Add(readByte);
                        break;
                    }
                    prevByte = readByte;
                    readByte = readBuffer[0];
                }

                if (uniqueBytes.Length > 0)
                {
                    var buffer = new byte[uniqueBytes.Length];
                    for (var j = 0; j < uniqueBytes.Length; j++)
                    {
                        buffer[j] = uniqueBytes[j];
                    }
                    // записываем количество серии уникальных байтов и указанием их количества со знаком минус
                    await WriteCompressedBytes(streamWriter, -buffer.Length, buffer);
                    uniqueBytes = new VectorArray<byte>(100, 100);
                }

                // считываем повторяющиеся символы
                while (readByte == prevByte)
                {
                    readCount++;
                    length++;
                    var readCnt = await streamReader.ReadAsync(readBuffer, 0, 1);
                    if(readCnt <= 0) break;
                    prevByte = readByte;
                    readByte = readBuffer[0];
                }

                if (length > 0)
                {
                    // как в обычном RLE записываем количество и значение байта
                    await WriteCompressedBytes(streamWriter, length + 1, prevByte);
                    length = 0;
                    prevByte = readByte;
                }
            }
        }

        /// <summary>
        /// Распаковка улучшенного сжатия
        /// </summary>
        /// <param name="streamReader"></param>
        /// <param name="streamWriter"></param>
        /// <returns></returns>
        protected override async Task DecompressInternal(FileStream streamReader, FileStream streamWriter)
        {
            var bytesCount = 0;
            while (bytesCount < streamReader.Length)
            {
                var lenBytes = new byte[4];
                // читаем длину серии 
                bytesCount += await streamReader.ReadAsync(lenBytes, 0, 4);
                var length = BitConverter.ToInt32(lenBytes);

                var readCnt = length < 0 ? Math.Abs(length) : 1;
                var valBytes = new byte[readCnt];
                bytesCount += await streamReader.ReadAsync(valBytes, 0, readCnt);
                if (length < 0)
                {
                    await streamWriter.WriteAsync(valBytes, 0, readCnt);
                    continue;
                }
                for (var i = 0; i < length; i++)
                {
                    await streamWriter.WriteAsync(valBytes, 0, 1);
                }
            }
        }
    }
}
