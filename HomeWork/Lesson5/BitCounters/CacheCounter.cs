namespace Lesson5.BitCounters
{
    /// <summary>
    /// Подсчет бит через кэш
    /// </summary>
    public sealed class CacheCounter : IBitCounter
    {
        private readonly int[] bits = new int[256];

        public CacheCounter()
        {
            var counter = new MinusOneCounter();
            for (int i = 0; i < 256; i++)
            {
                bits[i] = counter.CountBits((ulong)i);
            }
        }
        public int CountBits(ulong availableMoves)
        {
            int cnt = 0;
            while (availableMoves > 0)
            {
                // получаем количество единичных бит из кэша для первого октета числа
                cnt += bits[availableMoves & 255];
                // сдвигаем следующий 1 байт для поиска в кэше
                availableMoves >>= 8;
            }
            return cnt;
        }
    }
}
