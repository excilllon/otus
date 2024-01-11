namespace Lesson27.Compress
{
    public class RleCompress
    {
        /// <summary>
        /// Обычный алгоритм RLE для массивов
        /// </summary>
        /// <returns></returns>
        public byte[] SimpleCompress(byte[] source)
        {
            if (source == null || source.Length == 0) return source;

            var compressed = new byte[source.Length];
            var j = 0;
            byte lastCount = 1;
            for (var i = 0; i < source.Length; i++)
            {
                if (i == source.Length - 1 || source[i] != source[i + 1])
                {
                    compressed[j++] = lastCount;
                    compressed[j++] = source[i];
                    lastCount = 0;
                }
                lastCount++;
            }

            return compressed;
        }
    }
}
