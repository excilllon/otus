using Lesson24;

string[,] searches = 
{
    { "Hello, World!", "llo" },
    { "STRONGSIRING!", "RING" },
    { "AABAACAADAABAABA", "AADAAB" },
    { "GCATCGCAGAGAGTATACAGTACG", "GCAGAGAG" },
    { "PROTOKOLOLKOLOKOL", "KOLOKOL" },
    { "CBDBAAAABBBGGGGDDDBNOKLDDFPCBDBAAAABBBGGGGDDDBNOK", "FPCBDBA" },
};

for (var row = 0; row < searches.GetLength(0); row++)
{
    Console.WriteLine("===============================================");
    
    Console.WriteLine($"Поиск {searches[row, 1]} в {searches[row, 0]}");
    Console.Write("Полным перебором: ");
    Console.WriteLine(searches[row, 0].FullScanSearch(searches[row, 1]));

    Console.Write("сдвиги по префиксу шаблона (алгоритм Бойера-Мура-Хорспула): ");
    Console.WriteLine(searches[row, 0].SearchWithBMH(searches[row, 1]));

    Console.Write("Алгоритм Бойера-Мура: ");
    Console.WriteLine(searches[row, 0].SearchBoyerMoore(searches[row, 1]));

    Console.Write("String.IndexOf: ");
    Console.WriteLine(searches[row, 0].IndexOf(searches[row, 1]));
}

Console.ReadKey();
