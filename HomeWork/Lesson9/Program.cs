using BenchmarkDotNet.Running;
using Lesson9;
using Lesson9.Benchmarks;


int val = 0;
string userInput = null;
do
{
    Console.Write("Введите целочисленное неотрицательное N (длина массива):");
    userInput = Console.ReadLine();
} while (!int.TryParse(userInput, out val) || val < 0);
string? userChoice = null;

start: Console.WriteLine("Выберите алгоритм или введите x, чтобы выйти: ");
Console.WriteLine("1. Блочная сортировка");
Console.WriteLine("2. Сортировка подсчётом");
Console.WriteLine("3. Поразрядная сортировка");
Console.WriteLine("4. Бенчмарк всех алгоритмов");
userChoice = Console.ReadLine();
var sortings = new Sortings();
sortings.GenerateRandom(val);
switch (userChoice)
{
    case "1":
        sortings.BucketSort();
        Console.WriteLine(sortings.PrintArray()); break;
    case "2":
        sortings.GenerateRandom(val);
        sortings.CountingSort();
        Console.WriteLine(sortings.PrintArray()); break; 
    case "3":
        sortings.GenerateRandom(val);
        sortings.RadixSort();
        Console.WriteLine(sortings.PrintArray()); break;
    case "4":
        BenchmarkRunner.Run<SortingsBenchmark>(); break;
    case "x":
        return;
    default: goto start;
}

goto start;