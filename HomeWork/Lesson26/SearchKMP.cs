namespace Lesson26
{
    internal static class SearchKMPExtension
    {
        /// <summary>
        /// Создание таблицы Пи-функции, неоптимальный вариант O(n^3)
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static int[] CreatePi(string pattern)
        {
            var pi = new int[pattern.Length + 1];
            for (var i = 0; i <= pattern.Length; i++)
            {
                var line = pattern.Left(i);
                for (var len = 0; len < i; len++)
                {
                    if (line.Left(len) == line.Right(len))
                        pi[i] = len;
                }
            }
            return pi;
        }

        /// <summary>
        /// Алгоритм Кнута-Морриса-Пратта
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int SearchKMP(this string value, string pattern)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(pattern)) return -1;
            var pi = CreatePiFast(pattern + "@" + value);
            for (var q = 0; q < pi.Length; q++)
            {
                // возвращаем первый найденный
                if (pi[q] == pattern.Length) return q - pattern.Length - pattern.Length - 1;
            }
            return -1;
        }

        /// <summary>
        /// Быстрое (O(n)) создание таблицы Пи-функции, 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static int[] CreatePiFast(string pattern)
        {
            var pi = new int[pattern.Length + 1];
            for (var i = 1; i < pattern.Length; i++)
            {
                var len = pi[i];
                while (len > 0 && pattern[len] != pattern[i])
                    len = pi[len];
                if (pattern[len] == pattern[i])
                    len++;
                pi[i + 1] = len;
            }
            return pi;
        }

    }
}
