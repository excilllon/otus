// See https://aka.ms/new-console-template for more information

using Lesson18;

var adjacent = new []
{
    new[] { 1, 4 }, new[] { 2 }, new[] { 3 }, new[] { 1 }, new[] { 3 }
};
Graph graph = new Graph(adjacent);
var components = graph.FindSCbyKosaraju();
Console.WriteLine(string.Join(", ", components));
Console.ReadKey();

