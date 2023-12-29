using Lesson19;

var adjMatrix = new[]
{
    new []
    {
        0, 1, 0, 0, 0, 0, 0, 0, 0, 0
    },
    new []
    {
        0, 0, 0, 0, 1, 0, 0, 0, 0, 0
    },
    new []
    {
        0, 0, 0, 1, 0, 0, 0, 0, 0, 0
    },
    new []
    {
        1, 1, 0, 0, 1, 1, 0, 0, 0, 0
    },
    new []
    {
        0, 0, 0, 0, 0, 0, 1, 0, 0, 0
    },
    new []
    {
        0, 0, 0, 0, 1, 0, 0, 1, 0, 0
    },
    new []
    {
        0, 0, 0, 0, 0, 0, 0, 1, 0, 0
    },
    new []
    {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    },
    new []
    {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 1
    },
    new []
    {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    }
};
var graph = new Graph(adjMatrix, 10);
var result = graph.TopologicalSort();
Console.WriteLine($"Levels = {result.Length}");
for (var rowIdx = 0; rowIdx < result.Length; rowIdx++)
{
    var row = result[rowIdx];
    Console.WriteLine($"Level = {rowIdx + 1}");
    foreach (var col in row)
    {
        if (col == -1) break;
        Console.Write(col);
        Console.Write(',');
    }
    Console.WriteLine();
}
Console.ReadKey();