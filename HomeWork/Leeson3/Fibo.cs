using System.Numerics;

namespace Lesson3
{
    /// <summary>
    /// Алгоритмы поиска чисел Фибоначчи
    /// </summary>
    public static class Fibo
    {
        /// <summary>
        /// Итеративный O(N) алгоритм поиска чисел Фибоначчи
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public static BigInteger FiboWithIterations(int N)
        {
            if (N <= 1)
                return N;
            BigInteger F0 = 0;
            BigInteger F1 = 1;
            BigInteger Fn = F0 + F1;
            for (int i = 2; i <= N; i++)
            {
                Fn = F0 + F1;
                F0 = F1;
                F1 = Fn;
            }
            return Fn;
        }

        /// <summary>
        /// Рекурсивный O(2^N) алгоритм поиска чисел Фибоначчи
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public static BigInteger FiboR(int N)
        {
            if (N <= 1)
                return N;
            return FiboR(N - 1) + FiboR(N - 2);
        }

        /// <summary>
        /// Алгоритм поиска чисел Фибоначчи по формуле золотого сечения
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public static double FiboGolden(int N)
        {
            if (N <= 1)
                return N;
            var pFi = 1.6180339887;
            return (Math.Pow(pFi, N) - Math.Pow(-pFi, -N)) / (2 * pFi - 1);
        }
    }
}
