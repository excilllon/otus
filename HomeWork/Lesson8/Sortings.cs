using System;

namespace Lesson8
{
    public sealed class Sortings
    {
        private int[] _array = Array.Empty<int>();

        public void SetArray(int[] array) => _array = array;
        public int[] GetArray() => _array;

        public void GenerateRandom(int len)
        {
            _array = new int[len];
            Random randNum = new Random(42);
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = randNum.Next(10, 350);
            }
        }

        public string PrintArray()
        {
            return string.Join(", ", _array);
        }

        /// <summary>
        /// Быстрая сортировка
        /// </summary>
        public void QuickSort()
        {
            QSort(0, _array.Length - 1);
        }

        private void QSort(int left, int right)
        {
            if (left >= right) return;
            int mid = Split(left, right);
            QSort(left, mid - 1);
            QSort(mid + 1, right);
        }

        private int Split(int left, int right)
        {
            int pivot = _array[right];
            int m = left - 1;
            for (int i = left; i <= right; i++)
            {
                if (_array[i] <= pivot)
                {
                    m++;
                    (_array[i], _array[m]) = (_array[m], _array[i]);
                }
            }
            return m;
        }
        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        public void MergeSort()
        {
            MSort(0, _array.Length - 1);
        }

        private void MSort(int left, int right)
        {
            if (left >= right) return;
            int mid = (left + right) / 2;
            MSort(left, mid);
            MSort(mid + 1, right);
            Merge(left, mid, right);
        }

        private void Merge(int left, int mid, int right)
        {
            int[] temp = new int[right - left + 1];
            int a = left;
            int b = mid + 1;
            int m = 0;
            while (a <= mid && b <= right)
                if (_array[a] > _array[b])
                    temp[m++] = _array[b++];
                else
                    temp[m++] = _array[a++];
            while (a <= mid)
                temp[m++] = _array[a++];
            while (b <= right)
                temp[m++] = _array[b++];
            for (int i = left; i <= right; i++)
            {
                _array[i] = temp[i - left];
            }
        }


    }
}
