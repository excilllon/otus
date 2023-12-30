namespace Lesson20
{
    public sealed class Sortings
    {
        /// <summary>
        /// Пирамидальная сортировка
        /// </summary>
        public static void HeapSort(Edge[] edges)
        {
            for (var h = edges.Length / 2 - 1; h >= 0; h--)
            {
                Heapify(h, edges.Length, edges);
            }

            for (var i = edges.Length - 1; i > 0; i--)
            {
                (edges[i], edges[0]) = (edges[0], edges[i]);
                Heapify(0, i, edges);
            }
        }

        private static void Heapify(int root, int size, Edge[] array)
        {
            var x = root;
            var l = 2 * x + 1;
            var r = 2 * x + 2;
            if (l < size && array[l].Weight > array[x].Weight) x = l;
            if (r < size && array[r].Weight > array[x].Weight) x = r;
            if (x == root) return;
            (array[x], array[root]) = (array[root], array[x]);
            Heapify(x, size, array);
        }
    }
}
