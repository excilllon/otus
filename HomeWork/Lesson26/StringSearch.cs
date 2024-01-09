namespace Lesson26
{
    internal static class StringSearch
    {
        private static char[] _alphabet;

        static StringSearch()
        {
            _alphabet = new char[128];
            for (byte i = 0; i < 128; i++)
            {
                _alphabet[i] = (char)i;
            }
        }

        /// <summary>
        /// Поиск в строке с построением конечного автомата и прохождения по нему
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int SearchStateMachine(this string value, string pattern)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(pattern)) return -1;

            var stateTable = CreateStateTable(pattern);
            var state = 0;
            for (var i = 0; i < value.Length; i++)
            {
                state = stateTable[state, value[i]];
                if (state == pattern.Length) return i - pattern.Length + 1;
            }
            return -1;
        }

        /// <summary>
        /// Построение матрицы состояние для конечного автомата
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static int[,] CreateStateTable(string pattern)
        {
            var res = new int[pattern.Length, _alphabet.Length];

            for (var i = 0; i < pattern.Length; i++)
            {
                foreach (var c in _alphabet)
                {
                    var line = Left(pattern, i) + c;
                    var k = i + 1;
                    while (Left(pattern, k) != Right(line, k))
                        k--;
                    res[i, c] = k;
                }
            }
            return res;
        }

        public static string Left(this string pattern, int count)
        {
            if (count > pattern.Length) count = pattern.Length;
            var result = new char[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = pattern[i];
            }
            return new string(result);
        }

        public static string Right(this string pattern, int count)
        {
            if (count > pattern.Length) count = pattern.Length;
            var result = new char[count];
            int j = 0;
            for (var i = pattern.Length - count; i < pattern.Length; i++)
            {
                result[j++] = pattern[i];
            }
            return new string(result);
        }
    }
}
