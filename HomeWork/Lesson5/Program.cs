
using Lesson5;
using Lesson5.BitCounters;

string positionStr;
int position;
do
{
    Console.WriteLine("Введите номер клетки с фигурой: ");
    positionStr = Console.ReadLine();
} while (!int.TryParse(positionStr, out position) || position < 0);

start: Console.WriteLine("Выберите фигуру: ");
Console.WriteLine("1. Конь");
Console.WriteLine("2. Король");
Console.WriteLine("3. Ладья");
string userChoice = Console.ReadLine();
var bitBoard = new Bitboard(new CacheCounter());
(int cnt, ulong positionsBits) res;
switch (userChoice)
{
    case "1":
        res = bitBoard.MoveKnight(position); break;
    case "2":
        res = bitBoard.MoveKing(position); break;
    case "3":
        res = bitBoard.MoveRook(position); break;
    default: goto start;
}

Console.WriteLine("Маска возможных ходов {0}, количество {1}", res.positionsBits, res.cnt);
Console.ReadKey();


