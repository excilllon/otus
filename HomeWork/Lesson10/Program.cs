// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Lesson10;
using Lesson10.Benchmarks;

//var _randomAvlTreeForRemove = new AVLTree();

//var random = new Random(42);
//var _randomData = new int[100];
//for (var i = 0; i < 100; i++)
//{
//    _randomData[i] = random.Next(10, 250);
//    _randomAvlTreeForRemove.Insert(_randomData[i]);
//}
//var _randomSearchOrRemove = new int[100 / 10];
//random = new Random(5);
//for (var i = 0; i < 100 / 10; i++)
//{
//    _randomSearchOrRemove[i] = _randomData[random.Next(0, 100)];
//}
//foreach (var data in _randomSearchOrRemove)
//{
//    _randomAvlTreeForRemove.Remove(data);
//}

BenchmarkRunner.Run<BSTreeBenchmark>();
BenchmarkRunner.Run<AVLTreeBenchmark>();
Console.ReadLine();
