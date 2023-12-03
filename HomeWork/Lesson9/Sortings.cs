using System;

namespace Lesson9
{
    public sealed class Sortings
    {
        private int[] _array = Array.Empty<int>();

        public void SetArray(int[] array) => _array = array;
        public int[] GetArray() => _array;

        public void GenerateRandom(int len, int max = 999)
        {
            _array = new int[len];
            Random randNum = new Random(42);
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = randNum.Next(10, max);
            }
        }

        public string PrintArray()
        {
            return string.Join(", ", _array);
        }

        /// <summary>
        /// Алгоритм блочной сортировки 
        /// </summary>
        public void BucketSort()
        {
            var max = FindMax();

            max++;
            LinkedList[] bucket = new LinkedList[_array.Length];
            foreach (var el in _array)
            {
                var indexInBucket = (long)el * (long)_array.Length / max;
                bucket[indexInBucket] = new LinkedList(bucket[indexInBucket], el);
                var curItem = bucket[indexInBucket];
                while (curItem.Next != null)
                {
                    if (curItem.Value <= curItem.Next.Value) break;

                    (curItem.Value, curItem.Next.Value) = (curItem.Next.Value, curItem.Value);
                    curItem = curItem.Next;
                }
            }

            int j = 0;
            foreach (var el in bucket)
            {
                var curItem = el;
                while (curItem != null)
                {
                    _array[j++] = curItem.Value;
                    curItem = curItem.Next;
                }
            }

        }

        private int FindMax()
        {
            int max = _array[0];
            for (int i = 1; i < _array.Length; i++)
                if (max < _array[i])
                    max = _array[i];
            return max;
        }

        /// <summary>
        /// Алгоритм сортировки подсчётом
        /// </summary>
        public void CountingSort()
        {
            var max = FindMax();
            int[] countsArray = new int[max + 1];
            foreach (var el in _array)
            {
                countsArray[el]++;
            }
            for (int i = 1; i < countsArray.Length; i++)
            {
                countsArray[i] = countsArray[i - 1] + countsArray[i];
            }

            var newArray = new int[_array.Length];
            for (int i = _array.Length - 1; i >= 0; i--)
            {
                newArray[--countsArray[_array[i]]] = _array[i];
            }

            _array = newArray;
        }

        /// <summary>
        /// Алгоритм поразрядной сортировки
        /// </summary>
        public void RadixSort()
        {
            var max = FindMax();
            for (var exp = 1; max / exp > 0; exp *= 10)
            {
                var countsArray = new int[10];
                foreach (var ar in _array)
                    countsArray[(ar / exp) % 10]++;

                for (var i = 1; i < 10; i++)
                    countsArray[i] += countsArray[i - 1];

                var newArray = new int[_array.Length];
                for (var i = _array.Length - 1; i >= 0; i--)
                {
                    newArray[--countsArray[(_array[i] / exp) % 10]] = _array[i];
                }

                _array = newArray;
            }
        }

        private class LinkedList
        {
            public int Value { get; set; }
            public LinkedList Next { get; set; }
            public LinkedList(LinkedList next, int value)
            {
                Next = next;
                Value = value;
            }
        }
    }
}
