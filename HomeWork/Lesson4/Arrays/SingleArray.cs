using System.Collections;

namespace Lesson4.Arrays
{
    public class SingleArray<E> : IArray<E>
    {
        private E[] _array = new E[0];
        
        public void Add(E item, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            var insertPosition = index > _array.Length ? _array.Length : index;
            E[] newArray = new E[_array.Length + 1];
            for (var i = 0; i <= _array.Length; i++)
            {
                if (insertPosition == i)
                {
                    newArray[i] = item;
                }
                var iShifted = insertPosition > i ? i : i + 1;
                if (iShifted > _array.Length) break;
                newArray[iShifted] = _array[i];
            }
            _array = newArray;
        }

        public E Remove(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            if (_array.Length == 0) return default(E);
            var deletePosition = index >= _array.Length ? _array.Length - 1 : index;
            E deleted = _array[deletePosition];
            if (_array.Length == 1)
            {
                _array = new E[0];
                return deleted;
            }

            var newArray = new E[_array.Length - 1];
            for (var i = 0; i < _array.Length; i++)
            {
                var iShifted = deletePosition > i ? i : i - 1;
                if (iShifted < 0 || deletePosition == i) continue;
                newArray[iShifted] = _array[i];
            }
            _array = newArray;
            return deleted;
        }

        public void Print()
        {
            new ArrayPrinter<E>().Print(_array);
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

        public int Length => _array.Length;
    }
}
