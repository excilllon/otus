namespace Lesson30
{
    /// <summary>
    /// Задача 4. Острова
    /// </summary>
    internal class Islands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public int CountIslands(int[,] matrix)
        {
            var res = 0;
            for (var row = 0; row < matrix.GetLength(0); row++)
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] != 1) continue;
                    DFS(row, col, matrix);
                    res++;
                }
            return res;
        }

        /// <summary>
        /// Обход матрицы вглубь
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="matrix"></param>
        private void DFS(int row, int col, int[,] matrix)
        {
            if (matrix[row, col] == 0) return;
            matrix[row, col] = 0;
            // проход "вверх" по строкам
            if (row > 0) DFS(row - 1, col, matrix);
            // проход "вниз" по строкам
            if (row < matrix.GetLength(0) - 1) DFS(row + 1, col, matrix);
            if (col < matrix.GetLength(1) - 1) DFS(row, col + 1, matrix);
            if (col > 0) DFS(row, col - 1, matrix);
        }
    }
}
