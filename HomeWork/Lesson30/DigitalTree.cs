namespace Lesson30
{
    /// <summary>
    /// 2. Цифровая ёлочка
    /// </summary>
    internal class DigitalTree
    {
        public int GetShortestPath(int[][] tree)
        {
            for (var level = tree.Length-2; level >= 0; level--)
            {
                for (var elIndex = 0; elIndex < tree[level].Length; elIndex++)
                {
                    tree[level][elIndex] = Math.Max(tree[level + 1][elIndex] + tree[level][elIndex],
                        tree[level + 1][elIndex + 1] + tree[level][elIndex]);
                }
            }
            return tree[0][0];
        }
    }
}
