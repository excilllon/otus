namespace Lesson30
{
    /// <summary>
    /// Задача 1. Раз/два горох
    /// </summary>
    internal sealed class OneTwoPeas
    {
        /// <summary>
        /// Считаем сумму двух дробей
        /// </summary>
        /// <param name="gopherFraction"></param>
        /// <param name="homaFraction"></param>
        /// <returns>результат в виде сокращенной дроби</returns>
        /// <exception cref="ArgumentException"></exception>
        public string? CalculateFractionSum(string gopherFraction, string homaFraction)
        {
            var gopherVal = new int[2];
            var homaVal = new int[2];
            if (!TryParse(gopherFraction, out gopherVal)) return null;
            if (!TryParse(homaFraction, out homaVal)) return null;

            if (gopherVal[0] / gopherVal[1] > 1 || homaVal[0] / homaVal[1] > 1)
                throw new ArgumentException("Одна из дробей больше единицы");

            var res = new[]
            {
                gopherVal[0]*homaVal[1]+homaVal[0]*gopherVal[1], gopherVal[1]*homaVal[1]
            };
            var gcd = GreaterComDiv(res[0], res[1]);
            return $"{res[0] / gcd}/{res[1] / gcd}";
        }

        private int GreaterComDiv(int a, int b)
        {
            if (a == b) return a;
            if (a == 0) return b;
            if (b == 0) return a;

            if (IsEven(a) && IsEven(b)) return GreaterComDiv(a >> 1, b >> 1) << 1;
            if (IsEven(a) && !IsEven(b)) return GreaterComDiv(a >> 1, b);
            if (!IsEven(a) && IsEven(b)) return GreaterComDiv(a, b >> 1);
            if (a > b) return GreaterComDiv((a - b) >> 1, b);
            return GreaterComDiv(a, (b - a) >> 1);
        }

        private bool IsEven(int val)
        {
            return val % 2 == 0;
        }

        public bool IsValidFraction(string fraction)
        {
            return TryParse(fraction, out _);
        }

        private bool TryParse(string fraction, out int[] value)
        {
            value = new int[2];
            if (string.IsNullOrEmpty(fraction?.Trim())) return false;
            var parts = fraction.Split('/');
            if (parts.Length != 2) return false;

            if (!int.TryParse(parts[0], out value[0])) return false;
            if (!int.TryParse(parts[1], out value[1])) return false;
            return true;
        }

    }
}
