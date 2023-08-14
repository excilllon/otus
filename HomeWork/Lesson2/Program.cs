// See https://aka.ms/new-console-template for more information

using Lesson2;

int val = 0;
string userInput = null;
do
{
    Console.Write("Введите целочисленное неотрицательное N:");
    userInput = Console.ReadLine();
} while (!int.TryParse(userInput, out val) || val < 0);
Console.WriteLine(HappyTickets.GetCount(val));
Console.ReadKey();
