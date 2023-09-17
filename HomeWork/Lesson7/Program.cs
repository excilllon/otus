using BenchmarkDotNet.Running;
using Lesson7;
using Lesson7.Benchmarks;

int val = 0;
string userInput = null;
do
{
    Console.Write("Введите целочисленное неотрицательное N (длина массива):");
    userInput = Console.ReadLine();
} while (!int.TryParse(userInput, out val) || val < 0);
string? userChoice = null;

start: Console.WriteLine("Выберите алгоритм или введите x, чтобы выйти: ");
Console.WriteLine("1. Сортировка выбором");
Console.WriteLine("2. Пирамидальная сортировка");
Console.WriteLine("3. Бенчмарк всех алгоритмов");
userChoice = Console.ReadLine();
var sortings = new Sortings();
sortings.GenerateRandom(val);
switch (userChoice)
{
    case "1":
        sortings.SelectionSort();
        Console.WriteLine(sortings.PrintArray()); break;
    case "2":
        sortings.GenerateRandom(val);
        sortings.HeapSort();
        Console.WriteLine(sortings.PrintArray()); break;
    case "3":
        BenchmarkRunner.Run<SortingsBenchmark>(); break;
    case "x":
        return;
    default: goto start;
}

goto start;