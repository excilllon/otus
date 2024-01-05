namespace Lesson24
{
    public static class StringSearch
    {
        private const int AlphabetSize = 128;
        /// <summary>
        /// Поиск полным перебором
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int FullScanSearch(this string value, string pattern)
        {
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(value)) return -1;

            for (var i = 0; i <= value.Length - pattern.Length; i++)
            {
                var matched = true;
                for (var j = 0; j < pattern.Length && matched; j++)
                {
                    if (value[i + j] != pattern[j]) matched = false;
                }

                if (matched) return i;
            }
            return -1;
        }

        public static int SearchWithBMH(this string value, string pattern)
        {
            var offsetTable = CreateOffsetTable(pattern);
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(value)) return -1;

            for (var i = 0; i <= value.Length - pattern.Length; i += offsetTable[value[i + pattern.Length - 1]])
            {
                var matched = true;
                for (var j = pattern.Length - 1; j >= 0 && matched; j--)
                {
                    if (value[i + j] != pattern[j]) matched = false;
                }

                if (matched) return i;
            }
            return -1;
        }

        private static int[] CreateOffsetTable(string pattern)
        {
            var shift = new int[AlphabetSize];
            for (var i = 0; i < shift.Length; i++)
            {
                shift[i] = pattern.Length;
            }

            for (var m = 0; m < pattern.Length - 1; m++)
            {
                shift[pattern[m]] = pattern.Length - m - 1;
            }

            return shift;
        }
    }
}
