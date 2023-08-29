using System.Collections;

namespace Lesson4.Arrays
{
    public sealed class ArrayListAdapter: IArray<object>
    {
        private ArrayList _array = new ArrayList();
        public IEnumerator<object> GetEnumerator()
        {
            return ((IEnumerable<object>)_array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(object item, int index)
        {
            _array.Insert(index, item);
        }

        public object Remove(int index)
        {
            if (index >= _array.Count) return null;
            var item = _array[index];
            _array.RemoveAt(index);
            return item;
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public object this[int key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int Length { get; }
    }
}
