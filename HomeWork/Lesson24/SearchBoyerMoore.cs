namespace Lesson24
{
    /// <summary>
    /// Алгоритм Бойера-Мура
    /// </summary>
    internal static class SearchBoyerMooreExtension
    {
        private const int AlphabetSize = 128;
        /// <summary>
        /// Поиск алгоритмом Боейра-Мура
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int SearchBoyerMoore(this string value, string pattern)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(pattern)) return -1;

            var charTable = CreateBadCharTable(pattern);
            var goodSuffixTable = CreateGoodSuffixTable(pattern);
            for (var strIdx = pattern.Length - 1; strIdx < value.Length;)
            {
                int patIdx;
                for (patIdx = pattern.Length - 1; pattern[patIdx] == value[strIdx]; strIdx--, patIdx--)
                {
                    if (patIdx == 0) return strIdx;
                }
                // Применяем одну из эвристик
                strIdx += Math.Max(goodSuffixTable[pattern.Length - patIdx - 1], charTable[value[strIdx]]);
            }
            return -1;
        }

        /// <summary>
        /// Таблица сдвигов плохих символов (Эвристика стоп-символа)
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static int[] CreateBadCharTable(string pattern)
        {
            var table = new int[AlphabetSize];
            for (var i = 0; i < table.Length; i++)
            {
                table[i] = pattern.Length;
            }
            for (var m = 0; m < pattern.Length; m++)
            {
                table[pattern[m]] = pattern.Length - m - 1;
            }
            return table;
        }

        /// <summary>
        /// Таблица сдвигов хороших суффиксов (Эвристика совпавшего суффикса)
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static int[] CreateGoodSuffixTable(string pattern)
        {
            var table = new int[pattern.Length];
            var lastPrefixPosition = pattern.Length;
            for (var m = pattern.Length; m > 0; m--)
            {
                if (IsPrefix(pattern, m))
                {
                    lastPrefixPosition = m;
                }
                table[pattern.Length - m] = lastPrefixPosition - m + pattern.Length;
            }
            for (var m = 0; m < pattern.Length - 1; m++)
            {
                var suffixLength = SuffixLength(pattern, m);
                table[suffixLength] = pattern.Length - 1 - m + suffixLength;
            }
            return table;
        }

        private static bool IsPrefix(string pattern, int m)
        {
            for (int i = m, j = 0; i < pattern.Length; i++, j++)
            {
                if (pattern[i] != pattern[j])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Длина суффикса
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private static int SuffixLength(string pattern, int m)
        {
            int len = 0;
            for (int i = m, j = pattern.Length - 1; i >= 0 && pattern[i] == pattern[j]; i--, j--)
            {
                len ++;
            }
            return len;
        }
    }
}
