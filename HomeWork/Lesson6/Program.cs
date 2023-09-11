// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Lesson6;
using Lesson6.Benchmarks;

int val = 0;
string userInput = null;
do
{
    Console.Write("Введите целочисленное неотрицательное N (длина массива):");
    userInput = Console.ReadLine();
} while (!int.TryParse(userInput, out val) || val < 0);
string? userChoice = null;

start: Console.WriteLine("Выберите алгоритм или введите x, чтобы выйти: ");
Console.WriteLine("1. Простая сортировка пузырьком");
Console.WriteLine("2. Оптимизированная сортировка пузырьком");
Console.WriteLine("3. Сортировка вставкой");
Console.WriteLine("4. Сортировка вставкой со сдвигом");
Console.WriteLine("5. Сортировка вставкой со бинарном поиском");
Console.WriteLine("6. Сортировка Шелла");
Console.WriteLine("7. Сортировка Шелла (последовательность Hibbard)");
Console.WriteLine("8. Сортировка Шелла (последовательность Ciura)");
Console.WriteLine("9. Бенчмарк всех алгоритмов");
userChoice = Console.ReadLine();
var sortings = new Sortings();
sortings.GenerateRandom(val);
switch (userChoice)
{
    case "1":
        sortings.Bubble();
        Console.WriteLine(sortings.PrintArray()); break;
    case "2":
        sortings.BubbleOptimized();
        Console.WriteLine(sortings.PrintArray()); break;
    case "3":
        sortings.Insertion(); 
        Console.WriteLine(sortings.PrintArray()); break;
    case "4": 
        sortings.InsertionShift(); 
        Console.WriteLine(sortings.PrintArray()); break;
    case "5":
        sortings.InsertionBinary(); 
        Console.WriteLine(sortings.PrintArray()); break;
    case "6":
        sortings.Shell(); 
        Console.WriteLine(sortings.PrintArray()); break;
    case "7":
        sortings.ShellHibbard(); 
        Console.WriteLine(sortings.PrintArray()); break;
    case "8":
        sortings.ShellCiura(); 
        Console.WriteLine(sortings.PrintArray()); break;
    case "9":
        BenchmarkRunner.Run<SortingsBenchmark>(); break;
    case "x":
        return;
    default: goto start;
}

goto start;

