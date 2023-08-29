using System.Collections;

namespace Lesson4.Arrays
{
    public sealed class MatrixArray<E> : IArray<E>
    {
        private int _size;
        private int _vector;
        private IArray<IArray<E>> _array;
        private Dictionary<int, int> _columnCache = new Dictionary<int, int>();
        private Dictionary<int, int> _rowCache = new Dictionary<int, int>();


        public MatrixArray(int vector)
        {
            this._vector = vector;
            _array = new SingleArray<IArray<E>>();
            _size = 0;
        }

        public void Add(E item, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            if (_size == _array.Length * _vector)
            {
                _array.Add(new VectorArray<E>(_vector, _vector), _array.Length);
            }
            var swapItem = item;
            var insertPosition = index > _size ? _size : index;
            for (var i = insertPosition; i <= _size; i++)
            {
                (_array[i / _vector][i % _vector], swapItem) = (swapItem, _array[i / _vector][i % _vector]);
            }
            _size++;
        }

        public E Remove(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            if (_size == 0) return default(E);
            var deletePosition = index >= _size ? _size - 1 : index;
            E deleted = _array[deletePosition / _vector][deletePosition % _vector];
            if (deletePosition == _size - 1)
            {
                _array[deletePosition / _vector][deletePosition % _vector] = default(E);
                _size--;
                return deleted;
            }
            for (var i = deletePosition; i < _size; i++)
            {
                _array[i / _vector][i % _vector] = i + 1 >= _size ? default(E) : _array[(i + 1) / _vector][(i + 1) % _vector];
            }
            // Размер уменьшаем, емкость не меняем
            _size--;
            return deleted;
        }

        public void Print()
        {
            Console.WriteLine();
            foreach (var subArray in _array)
            {
                Console.WriteLine(string.Join(", ", subArray));
            }
            Console.WriteLine();
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
            get => _array[key / _vector][key % _vector];
            set => _array[key][key % _vector] = value;
        }

        public int Length => _array.Length;
    }
}
