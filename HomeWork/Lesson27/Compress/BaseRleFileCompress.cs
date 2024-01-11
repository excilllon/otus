namespace Lesson27.Compress
{
    /// <summary>
    /// Сжатие файлов "простым" алгоритмом RLE
    /// </summary>
    internal class BaseRleFileCompress
    {
        public const string ArchiveExtension = "rlea";

        /// <summary>
        /// Сжатие указанного файла по его имени.
        /// В результате будет получен файл с название "исходное_имя_файла".rle 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task Compress(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            var newFileName = fileName + $".{ArchiveExtension}";
            if (File.Exists(newFileName)) File.Delete(newFileName);

            await using var streamReader = File.OpenRead(fileName);
            await using var streamWriter = File.OpenWrite(newFileName);
            await CompressInternal(streamReader, streamWriter);
        }

        protected virtual async Task CompressInternal(FileStream streamReader, FileStream streamWriter)
        {
            var length = 1;
            var prevByte = streamReader.ReadByte();
            for (var i = 1; i < streamReader.Length; i++)
            {
                var readByte = streamReader.ReadByte();
                // если серия одинаковых символов прервалась...
                if (readByte != prevByte)
                {
                    await WriteCompressedBytes(streamWriter, length, (byte)prevByte);
                    length = 0;
                }

                length++;
                // дозаписываем последний байт и количество его повторов
                if (i == streamReader.Length - 1)
                {
                    await WriteCompressedBytes(streamWriter, length, (byte)readByte);
                }
                prevByte = readByte;
            }
        }

        protected static async Task WriteCompressedBytes(FileStream streamWriter, int length, params byte[] prevBytes)
        {
            // ... записываем вначале 4 байта длины серии
            // (целое Int32 так как меньшего типа данных может не хватит для больших файлов)
            var lengthInBytes = BitConverter.GetBytes(length);
            await streamWriter.WriteAsync(lengthInBytes, 0, lengthInBytes.Length);
            // следом сразу же записываем один байт значения
            await streamWriter.WriteAsync(prevBytes, 0, prevBytes.Length);
        }

        /// <summary>
        /// Распаковка ранее сжатого файла
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task Decompress(string fileName)
        {
            if (Path.GetExtension(fileName)?.ToLowerInvariant() != $".{ArchiveExtension}") return;

            var newFileName = fileName.Replace($".{ArchiveExtension}", null);
            if (File.Exists(newFileName)) File.Delete(newFileName);
            await using var streamReader = File.OpenRead(fileName);
            await using var streamWriter = File.OpenWrite(newFileName);
            await DecompressInternal(streamReader, streamWriter);
        }

        protected virtual async Task DecompressInternal(FileStream streamReader, FileStream streamWriter)
        {
            var bytesCount = 0;
            while (bytesCount < streamReader.Length)
            {
                var lenBytes = new byte[4];
                // читаем длину серии 
                bytesCount += await streamReader.ReadAsync(lenBytes, 0, 4);
                var length = BitConverter.ToInt32(lenBytes);
                // читаем значение и записываем его нужное количество раз
                var valBytes = new byte[1];
                bytesCount += await streamReader.ReadAsync(valBytes, 0, 1);
                for (var i = 0; i < length; i++)
                {
                    await streamWriter.WriteAsync(valBytes, 0, 1);
                }
            }
        }
    }
}
