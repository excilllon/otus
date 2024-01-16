namespace Lesson30
{
    /// <summary>
    /// Задача 3. Пятью-восемь
    /// </summary>
    internal class FiveAndEight
    {
        /// <summary>
        /// Считаем  количество возможных чисел из 5 и 8
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int GetCounts(int n)
        {
            var counts = new int[4] { 1, 0, 1, 0 };
            for (var i = 1; i < n; i++)
            {
                var prev5 = counts[0];
                counts[0] = counts[3] + counts[2];
                var prev55 = counts[1];
                counts[1] = prev5;
                var prev8 = counts[2];
                counts[2] = prev5 + prev55;
                counts[3] = prev8;
            }
            return counts[0] + counts[1] + counts[2] + counts[3];
        }
    }
}
