namespace Lesson5.BitCounters
{
    /// <summary>
    /// Подсчет вычитанием единицы и последующей конъюнкцией с полученным числом
    /// </summary>
    public sealed class MinusOneCounter: IBitCounter
    {
        public int CountBits(ulong availableMoves)
        {
            int count = 0;
            while (availableMoves != 0)
            {
                availableMoves &= (availableMoves - 1);
                count ++;
            }
            return count;
        }
    }
}
