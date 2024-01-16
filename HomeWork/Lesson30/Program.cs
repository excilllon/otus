using Lesson30;

start: Console.WriteLine("Выберите задачу: ");
Console.WriteLine("1. Раз/два горох");
Console.WriteLine("2. Цифровая ёлочка");
Console.WriteLine("3. Пятью-восемь");
Console.WriteLine("4. Острова ");
string userChoice = Console.ReadLine();
switch (userChoice)
{
    case "1":
        var oneTwoPeas = new OneTwoPeas();
        Console.WriteLine("Сколько набрал суслик гороха? (a/b):");
        var gopherFraction = Console.ReadLine();
        if (!oneTwoPeas.IsValidFraction(gopherFraction))
        {
            Console.WriteLine("Дробь некорректна!");
            goto start;
        }
        Console.WriteLine("Сколько набрал хома гороха? (c/d):");
        var homaFraction = Console.ReadLine();
        if (!oneTwoPeas.IsValidFraction(homaFraction))
        {
            Console.WriteLine("Дробь некорректна!");
            goto start;
        }
        Console.WriteLine("Результат: {0}", oneTwoPeas.CalculateFractionSum(gopherFraction, homaFraction));
        break;
    case "2":
        var digitalTree = new DigitalTree();
        Console.WriteLine("Введите высоту ёлочки:");
        var height = Console.ReadLine();
        if (!int.TryParse(height, out var heightVal))
        {
            Console.WriteLine("Высота некорректна!");
            goto start;
        }

        var tree = new int[heightVal][];
        for (var i = 0; i < heightVal; i++)
        {
            Console.WriteLine($"Введите уровень {i + 1} елочки: ");
            var levelElementsStr = Console.ReadLine();
            var levelElements = levelElementsStr?.Split(' ');
            if ((levelElements?.Length ?? 0) == 0 || levelElements.Length != i + 1)
            {
                Console.WriteLine($"Количество элементов для уровня {i + 1} некорректно!");
                goto start;
            }

            tree[i] = new int[levelElements.Length];
            for (var j = 0; j < levelElements.Length; j++)
            {
                tree[i][j] = int.Parse(levelElements[j]);
            }
        }

        var res = digitalTree.GetShortestPath(tree);
        Console.WriteLine(res);
        break;
    case "3":
        var fiveAndEight = new FiveAndEight();
        Console.WriteLine("Введите N:");
        var nStr = Console.ReadLine();
        if (!int.TryParse(nStr, out var n))
        {
            Console.WriteLine("N некорректна!");
            goto start;
        }
        Console.WriteLine(fiveAndEight.GetCounts(n));
        break;
    case "4":
        var islands = new Islands();
        Console.WriteLine("Введите размер матрицы:");
        var sizeStr = Console.ReadLine();
        if (!int.TryParse(sizeStr, out var size))
        {
            Console.WriteLine("Размер некорректен!");
            goto start;
        }
        var matrix = new int[size, size];
        for (var i = 0; i < size; i++)
        {
            Console.WriteLine($"Введите строку {i + 1} матрицы: ");
            var rowValuesStr = Console.ReadLine();
            var rowValues = rowValuesStr?.Split(' ');
            if ((rowValues?.Length ?? 0) == 0 || rowValues.Length != size)
            {
                Console.WriteLine($"Количество значений в строке некорректно!");
                goto start;
            }
            for (var j = 0; j < size; j++)
            {
                matrix[i, j] = int.Parse(rowValues[j]);
                if (matrix[i, j] != 0 && matrix[i, j] != 1)
                {
                    Console.WriteLine($"Необходимо указать 0 или 1!");
                    goto start;
                }
            }
        }
        Console.WriteLine(islands.CountIslands(matrix));
        break;
    default: goto start;
}

Console.WriteLine();
goto start;