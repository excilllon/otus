using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2
{
    public static class HappyTickets
    {
        public static long GetCount(int N)
        {
            var sumCounts = new long[N + 1][];
            sumCounts[0] = new long[] { 1 };
            long result = 0;
            for (var i = 1; i <= N; i++)
            {
                sumCounts[i] = new long[i * 9 + 1];
                result = 0;
                for (var j = 0; j <= i * 9; j++)
                {
                    for (var l = 0; l <= 9; l++)
                    {
                        if (j < l || j - l > (i - 1) * 9) continue;
                        sumCounts[i][j] += sumCounts[i - 1][j - l];
                    }
                    result += sumCounts[i][j] * sumCounts[i][j];
                }
            }

            return result;
        }
    }
}
