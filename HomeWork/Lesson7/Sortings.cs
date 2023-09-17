using System.ComponentModel.Design.Serialization;

namespace Lesson7
{
    public sealed class Sortings
    {
        private int[] _array = Array.Empty<int>();
        public void SetArray(int[] array)
        {
            _array = array;
        }

        public void GenerateRandom(int len)
        {
            _array = new int[len];
            Random randNum = new Random(42);
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = randNum.Next(10,350);
            }
        }
        /// <summary>
        /// Сортировка выбором
        /// </summary>
        public void SelectionSort()
        {
            for (var i = _array.Length - 1; i > 0; i--)
            {
                var max = 0;
                for (var j = 1; j <= i; j++)
                    if (_array[j] > _array[max])
                        max = j;
                (_array[i], _array[max]) = (_array[max], _array[i]);
            }
        }
        /// <summary>
        /// Пирамидальная сортировка
        /// </summary>
        public void HeapSort()
        {
            for (var h = _array.Length / 2 - 1; h >= 0; h--)
            {
                Heapify(h, _array.Length);
            }

            for (var i = _array.Length - 1; i > 0; i--)
            {
                (_array[i], _array[0]) = (_array[0], _array[i]);
                Heapify(0, i);
            }
        }

        private void Heapify(int root, int size)
        {
            var x = root;
            var l = 2 * x + 1;
            var r = 2 * x + 2;
            if (l < size && _array[l] > _array[x]) x = l;
            if (r < size && _array[r] > _array[x]) x = r;
            if (x == root) return;
            (_array[x], _array[root]) = (_array[root], _array[x]);
            Heapify(x, size);
        }

        public string PrintArray()
        {
            return string.Join(", ", _array);
        }
    }
}
