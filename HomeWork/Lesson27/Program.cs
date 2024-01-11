using Lesson27;
using Lesson27.Compress;

void ShowHelp()
{
    Console.WriteLine("Программа для сжатия/распаковки файлов по алгоритму RLE");
    Console.WriteLine("Используйте следующие параметры:");
    Console.WriteLine("-c <путь к файлу> - для сжатия указанного файла, например rle -a C:\\doc.txt");
    Console.WriteLine("-cs <путь к файлу> - для сжатия указанного файла обычным алгоритмом RLE, например rle -a C:\\doc.txt");
    Console.WriteLine("-d <путь к файлу> - для распаковки указанного файла, например rle -d C:\\doc.txt.rlea");
}

if ((args?.Length ?? 0) == 0)
{
    ShowHelp();
    return;
}

if (args.Length == 2)
{
    string fileName = args[1];
    if (!File.Exists(fileName))
    {
        Console.WriteLine($"Файл {fileName} не найден");
        return;
    }

    var fileCompress = args[0].ToLowerInvariant() == "-cs" ? new BaseRleFileCompress() : new RleFileCompress();

    switch (args[0].ToLowerInvariant())
    {
        case "-c": 
        case "-cs": await fileCompress.Compress(fileName); break;
        case "-d": await fileCompress.Decompress(fileName); break;
        default: ShowHelp(); break;
    }
}

