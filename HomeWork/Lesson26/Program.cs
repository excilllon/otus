using Lesson26;

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
    Console.Write("Конечный автомат: ");
    Console.WriteLine(searches[row, 0].SearchStateMachine(searches[row, 1]));

    Console.Write("\"Медленный\" Алгоритм Кнута-Морриса-Пратта: ");
    Console.WriteLine(searches[row, 0].SearchKMPSlow(searches[row, 1]));

    Console.Write("Алгоритм Кнута-Морриса-Пратта: ");
    Console.WriteLine(searches[row, 0].SearchKMP(searches[row, 1]));

    Console.Write("String.IndexOf: ");
    Console.WriteLine(searches[row, 0].IndexOf(searches[row, 1]));
}

Console.ReadKey();