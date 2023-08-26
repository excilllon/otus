// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Lesson3;
using Lesson3.Benchmarks;

string? userChoice = null;
start: Console.WriteLine("Выберите алгоритм или введите x, чтобы выйти: ");
Console.WriteLine("1. Фибоначчи");
Console.WriteLine("2. Возведение в степень");
Console.WriteLine("3. Подсчет простых чисел");
Console.WriteLine("4. Бенчмаркинг алгоритмов Фибоначчи");
Console.WriteLine("5. Бенчмаркинг алгоритмов возведения в степень");
Console.WriteLine("6. Бенчмаркинг алгоритмов поиска простых чисел");
userChoice = Console.ReadLine();
switch (userChoice)
{
    case "1":
        FiboChoice(); break;
    case "2":
        PowChoice(); break;
    case "3":
        PrimesChoice(); break;
    case "4": BenchmarkRunner.Run<FiboBenchmark>(); break;
    case "5": BenchmarkRunner.Run<PowerBenchmark>(); break;
    case "6": BenchmarkRunner.Run<PrimesBenchmark>(); break;
    case "x":
        return;
    default: goto start;
}

goto start;

void FiboChoice()
{
    int val;
    string? userInput;
    do
    {
        Console.Write("Введите целочисленное неотрицательное N:");
        userInput = Console.ReadLine();
    } 
    while (!int.TryParse(userInput, out val) || val < 0);

    Console.WriteLine("Итеративный O(N) алгоритм поиска чисел Фибоначчи: {0}", Fibo.FiboWithIterations(val));
    Console.WriteLine("Рекурсивный O(2^N) алгоритм поиска чисел Фибоначчи: {0}", Fibo.FiboR(val));
    Console.WriteLine("Алгоритм поиска чисел Фибоначчи по формуле золотого сечения: {0}", Fibo.FiboGolden(val));
}

void PowChoice()
{
    double val;
    long pow;
    string? userInputPower;
    string? userInputVal;
    do
    {
        Console.Write("Введите N:");
        userInputVal = Console.ReadLine();
        Console.Write("Введите степень:");
        userInputPower = Console.ReadLine();
    } 
    while (!double.TryParse(userInputVal, out val) || !long.TryParse(userInputPower, out pow));

    Console.WriteLine("Итеративный O(N) алгоритм возведения числа в степень: {0}", Power.PowerWithIterations(val, pow));
    Console.WriteLine("Алгоритм возведения в степень через двоичное разложение показателя степени: {0}", Power.PowBinary(val, pow));
}

void PrimesChoice()
{
    long val;
    string? userInput;
    do
    {
        Console.Write("Введите натуральное N:");
        userInput = Console.ReadLine();
    } 
    while (!long.TryParse(userInput, out val) || val <= 0);

    Console.WriteLine("Поиск количества простых чисел через перебор делителей: {0}", PrimeNumbers.CountPrimesIterations(val));
    Console.WriteLine("Алгоритм \"Решето Эратосфена\" для быстрого поиска простых чисел: {0}", PrimeNumbers.CountEratosthenes(val));
    Console.WriteLine("Решето Эратосфена со сложностью O(n): {0}", PrimeNumbers.CountEratosthenesOptimized(val));
}
