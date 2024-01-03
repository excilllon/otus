namespace Lesson21
{
    /// <summary>
    /// Очередь с приоритетом на основе min-кучи (в корне кучи элементы с меньшим значение приоритета)
    /// </summary>
    internal sealed class PriorityQueue
    {
        private int[][] _queue;
        private int _count;

        public PriorityQueue(int capacity)
        {
            _queue = new int[capacity][];
        }

        public void Enqueue(int value, int priority)
        {
            _queue[_count] = new[] { value, priority };
            var newItemIndex = _count;
            _count++;

            // Вместо рекурсивного Heapify будем восстанавливать кучу в цикле,
            // начиная с добавленного элемента
            while (newItemIndex > 0)
            {
                var parentIndex = (newItemIndex - 1) / 2;
                if (_queue[newItemIndex][1] >= _queue[parentIndex][1])
                    break;

                (_queue[newItemIndex], _queue[parentIndex]) = (_queue[parentIndex], _queue[newItemIndex]);

                newItemIndex = parentIndex;
            }
        }

        public (int value , int priority) Dequeue()
        {
            // забираем корневой элемент
            var topItem = _queue[0];
            // и перемещаем на его место последний, иначе придется сдвигать элементы массива вначало за O(n)
            _queue[0] = _queue[_count-1];
            _queue[_count - 1] = null;
            _count--;

            // Далее восстанавливаем кучу
            Heapify(0, _count);
 
            return (topItem[0], topItem[1]);
        }

        public bool Empty()
        {
            return _count == 0;
        }

        private void Heapify(int root, int size)
        {
            var x = root;
            var l = 2 * x + 1;
            var r = 2 * x + 2;
            if (l < size && _queue[l][1] < _queue[x][1]) x = l;
            if (r < size && _queue[r][1] < _queue[x][1]) x = r;
            if (x == root) return;
            (_queue[x], _queue[root]) = (_queue[root], _queue[x]);
            Heapify(x, size);
        }

    }
}
