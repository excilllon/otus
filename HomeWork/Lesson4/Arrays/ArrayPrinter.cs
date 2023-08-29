namespace Lesson4.Arrays
{
    public class ArrayPrinter<E>
    {
        public void Print(E[] array, int? size = null)
        {
            Console.WriteLine(string.Join(", ", array.Take(size == null ? array.Length : size.Value)));
        }
    }
}
