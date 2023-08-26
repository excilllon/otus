namespace Lesson3
{
    /// <summary>
    /// Алгоритмы возведения в степень
    /// </summary>
    public static class Power
    {
        /// <summary>
        /// Итеративный O(N) алгоритм возведения числа в степень
        /// </summary>
        /// <param name="value"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static double PowerWithIterations(double value, long power)
        {
            if (power == 0) return 1;

            double result = 1;
            for (long i = 0; i < (power < 0 ? -power : power); i++)
            {
                result *= value;
            }

            return power > 0 ? result : 1 / result;
        }

        /// <summary>
        /// Алгоритм возведения в степень через двоичное разложение показателя степени O(2LogN) = O(LogN)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static double PowBinary(double value, long power)
        {
            double result = 1;
            var absPower = power < 0 ? -power : power;
            while (absPower > 0)
            {
                if (absPower % 2 != 0)
                {
                    result *= value;
                }
                value *= value;
                absPower >>= 1;
            }
            return power > 0 ? result : 1 / result;
        }
    }
}
