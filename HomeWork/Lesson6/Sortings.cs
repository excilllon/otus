using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6
{
    public class Sortings
    {
        private int[] _array = Array.Empty<int>();
        private int[] _ciuraSequence = new[] { 1750, 701, 301, 132, 57, 23, 10, 4, 1 };
        public void SetArray(int[] array)
        {
            _array = array;
        }

        public void GenerateRandom(int len)
        {
            _array = new int[len];
            Random randNum = new Random();
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = randNum.Next();
            }
        }
        /// <summary>
        /// Простая сортировка пузырьком
        /// </summary>
        public void Bubble()
        {
            for (var i = _array.Length - 1; i > 0; i--)
                for (var j = 0; j < i; j++)
                {
                    if (_array[j] > _array[j + 1])
                        (_array[j], _array[j + 1]) = (_array[j + 1], _array[j]);
                }
        }
        /// <summary>
        /// Оптимизированная сортировка пузырьком
        /// </summary>
        public void BubbleOptimized()
        {
            int lastComparison = _array.Length - 1;
            for (var i = _array.Length - 1; i > 0; i--)
            {
                bool swapped = false;
                int currentSwap = -1;
                for (var j = 0; j < lastComparison; j++)
                {
                    if (_array[j] <= _array[j + 1]) continue;
                    (_array[j], _array[j + 1]) = (_array[j + 1], _array[j]);
                    swapped = true;
                    currentSwap = j;
                }
                // если обменов не было значит массив уже отсортирован
                if (!swapped) return;
                lastComparison = currentSwap;
            }
        }
        /// <summary>
        /// Сортировка вставкой
        /// </summary>
        public void Insertion()
        {
            for (var i = 1; i < _array.Length; i++)
            {
                for (var j = i - 1; j >= 0 && _array[j] > _array[j + 1]; j--)
                {
                    (_array[j], _array[j + 1]) = (_array[j + 1], _array[j]);
                }
            }
        }
        /// <summary>
        /// Сортировка вставкой со сдвигом
        /// </summary>
        public void InsertionShift()
        {
            int j;
            for (var i = 1; i < _array.Length; i++)
            {
                var k = _array[i];
                for (j = i - 1; j >= 0 && _array[j] > k; j--)
                {
                    _array[j + 1] = _array[j];
                }
                _array[j + 1] = k;
            }
        }
        /// <summary>
        /// Сортировка вставкой со бинарном поиском
        /// </summary>
        public void InsertionBinary()
        {
            int j;
            for (var i = 1; i < _array.Length; i++)
            {
                var k = _array[i];
                var p = BinarySearch(k, 0, i - 1);
                for (j = i - 1; j >= p; j--)
                {
                    _array[j + 1] = _array[j];
                }
                _array[j + 1] = k;
            }
        }

        private int BinarySearch(int key, int left, int right)
        {
            while (right > left)
            {
                var mid = (left + right) / 2;
                if (key >= _array[mid])
                {
                    left = mid + 1;
                }
                else right = mid - 1;
            }

            return key >= _array[left] ? left + 1 : left;
        }
        /// <summary>
        /// Сортировка Шелла
        /// </summary>
        public void Shell()
        {
            for (var gap = _array.Length / 2; gap > 0; gap /= 2)
                for (var j = gap; j < _array.Length; j++)
                    for (var i = j; i >= gap && _array[i - gap] > _array[i]; i -= gap)
                        (_array[i - gap], _array[i]) = (_array[i], _array[i - gap]);
        }
        /// <summary>
        /// Сортировка Шелла (последовательность Hibbard)
        /// </summary>
        public void ShellHibbard()
        {
            int k = (int)Math.Log(_array.Length, 2);
            for (var gap = (int)Math.Pow(2, k) - 1; gap > 0; gap = (int)Math.Pow(2, --k) - 1)
                for (var j = gap; j < _array.Length; j++)
                    for (var i = j; i >= gap && _array[i - gap] > _array[i]; i -= gap)
                        (_array[i - gap], _array[i]) = (_array[i], _array[i - gap]);
        }
        /// <summary>
        /// Сортировка Шелла (последовательность Ciura)
        /// </summary>
        public void ShellCiura()
        {
            foreach (var gap in _ciuraSequence)
            {
                for (var j = gap; j < _array.Length; j++)
                for (var i = j; i >= gap && _array[i - gap] > _array[i]; i -= gap)
                    (_array[i - gap], _array[i]) = (_array[i], _array[i - gap]);
            }
        }

        public string PrintArray()
        {
            return string.Join(", ", _array);
        }
    }
}
