namespace Lesson3
{
    /// <summary>
    /// Алгоритмы поиска простых чисел
    /// </summary>
    public static class PrimeNumbers
    {
        /// <summary>
        /// Поиск количества простых чисел через перебор делителей, O(N^2)
        /// </summary>
        /// <returns></returns>
        public static long CountPrimesIterations(long N)
        {
            long count = 0;
            for (long i = 2; i <= N; i++)
            {
                if (IsPrime(i))
                    count++;
            }
            return count;
        }

        private static bool IsPrime(long value)
        {
            if (value == 2) return true;
            if (value % 2 == 0) return false;
            int count = 0;
            for (long i = 3; i <= value; i += 2)
            {
                if (value % i == 0) count++;
            }
            return count == 1;
        }

        /// <summary>
        /// Алгоритм "Решето Эратосфена" для быстрого поиска простых чисел O(N Log Log N)
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public static long CountEratosthenes(long N)
        {
            bool[] checkedComposites = new bool[N + 1];
            long count = 0;
            for (long i = 2; i <= N; i++)
            {
                if (checkedComposites[i]) continue;
                count++;
                for (var j = i * i; j <= N; j += i)
                {
                    checkedComposites[j] = true;
                }
            }
            return count;
        }
        /// <summary>
        /// Решето Эратосфена со сложностью O(n)
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public static long CountEratosthenesOptimized(long N)
        {
            long[] lp = new long[N+1];
            long[] pr = new long[N];
            long count = 0;
            for (long i = 2; i <= N; i++)
            {
                if (lp[i] == 0)
                {
                    lp[i] = i;
                    pr[count++] = i;
                }
                long j = 0;
                var p = pr[j];
                while (p <= lp[i] && p * i <= N && p > 0)
                {
                    lp[p * i] = p;
                    p = pr[++j];
                }
            }
            return count;
        }
    }
}
