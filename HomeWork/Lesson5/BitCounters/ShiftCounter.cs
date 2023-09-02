namespace Lesson5.BitCounters
{
    /// <summary>
    /// Подсчет сдвигом на 1 бит
    /// </summary>
    public sealed class ShiftCounter: IBitCounter
    {
        public int CountBits(ulong availableMoves)
        {
            int cnt = 0;
            while (availableMoves > 0)
            {
                cnt += (int)availableMoves & 1;
                availableMoves >>= 1;
            }
            return cnt;
        }
    }
}
