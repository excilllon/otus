using System.Collections;

namespace Lesson4.Arrays
{
    public sealed class VectorArray<E> : IArray<E>
    {
        /// <summary>
        /// Факт. размер
        /// </summary>
        private int _size;
        private int _vector;
        private E[] _array = new E[0];

        public VectorArray(int vector)
        {
            _vector = vector;
        }

        public VectorArray(int vector, int initLength)
        {
            _vector = vector;
            _array = new E[initLength];
        }

        public VectorArray() : this(10)
        {
        }

        public void Add(E item, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            var insertPosition = index > _size ? _size : index;
            if (_size == _array.Length)
            {
                var newArray = new E[_array.Length + _vector];

                for (var i = 0; i <= _size; i++)
                {
                    if (insertPosition == i)
                    {
                        newArray[i] = item;
                    }

                    var iShifted = insertPosition > i ? i : i + 1;
                    if (iShifted > _size) break;
                    newArray[iShifted] = _array[i];
                }
                _array = newArray;
            }
            else
            {
                var swapItem = item;
                for (var i = insertPosition; i <= _size; i++)
                {
                    (_array[i], swapItem) = (swapItem, _array[i]);
                }
            }
            _size++;
        }

        public void Add(E item)
        {
            Add(item, _size);
        }

        public E Remove(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            if (_size == 0) return default(E);
            var deletePosition = index >= _size ? _size - 1 : index;
            E deleted = _array[deletePosition];
            if (deletePosition == _size - 1)
            {
                _size--;
                _array[deletePosition] = default(E);
                return deleted;
            }
            for (var i = deletePosition; i < _size; i++)
            {
                _array[i] = i + 1 >= _array.Length ? default(E) : _array[i + 1];
            }
            // Размер уменьшаем, емкость не меняем
            _size--;
            return deleted;
        }

        public void Print()
        {
            new ArrayPrinter<E>().Print(_array, _size);
        }

        public IEnumerator<E> GetEnumerator()
        {
            return ((IEnumerable<E>)_array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public E this[int key]
        {
            get => _array[key];
            set => _array[key] = value;
        }

        public int Length => _size;
    }
}
