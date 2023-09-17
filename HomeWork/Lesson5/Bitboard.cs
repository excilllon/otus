using Lesson5.BitCounters;

namespace Lesson5
{
    public sealed class Bitboard
    {
        private readonly IBitCounter _bitCounter;

        public Bitboard(IBitCounter bitCounter)
        {
            _bitCounter = bitCounter;
        }
        /// <summary>
        /// Ход конем
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public (int cnt, ulong positionsBits) MoveKnight(int position)
        {
            ulong bitsPos = 1UL << position;
            ulong notA = 0xFeFeFeFeFeFeFeFe;
            ulong notAB = 0xFcFcFcFcFcFcFcFc;
            ulong notH = 0x7f7f7f7f7f7f7f7f;
            ulong notGH = 0x3f3f3f3f3f3f3f3f;
            var availableMoves = notGH & (bitsPos << 6 | bitsPos >> 10)
                        | notH & (bitsPos << 15 | bitsPos >> 17)
                        | notA & (bitsPos << 17 | bitsPos >> 15)
                        | notAB & (bitsPos << 10 | bitsPos >> 6);

            return (_bitCounter.CountBits(availableMoves), availableMoves);

        }

        /// <summary>
        /// Ход королем
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public (int cnt, ulong positionsBits) MoveKing(int position)
        {
            ulong bitsPos = 1UL << position;
            ulong notA = 0xFeFeFeFeFeFeFeFe;
            ulong notH = 0x7f7f7f7f7f7f7f7f;
            var availableMoves = ((notA & bitsPos) << 7 | bitsPos << 8 | (notH & bitsPos) << 9)
                        | ((notA & bitsPos) >> 1 | (notH & bitsPos) << 1)
                        | ((notA & bitsPos) >> 9 | bitsPos >> 8 | (notH & bitsPos) >> 7);
            return (_bitCounter.CountBits(availableMoves), availableMoves);
        }

        /// <summary>
        /// Ход ладьей
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public (int cnt, ulong positionsBits) MoveRook(int position)
        {
            ulong verticalLine = 0x101010101010101;
            ulong horizontalLine = 0xff;
            var availableMoves = verticalLine << (position & 7) ^ (horizontalLine << (position >> 3 << 3));
            return (_bitCounter.CountBits(availableMoves), availableMoves);
        }
    }
}
