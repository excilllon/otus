namespace Lesson4.Arrays
{
    public interface IArray<T>: IEnumerable<T>
    {
        void Add(T item, int index); 
        T Remove(int index);
        void Print();
        T this[int key] { get; set; }
        int Length { get;  }
    }
}
