﻿using Lesson14;

Console.WriteLine("Хеш-таблица с открытой адресацией:");
var hashMapOpenAddress = new HashMapOpenAddress<string, int>(10);
Console.WriteLine($"Capacity: {hashMapOpenAddress.Capacity}");
hashMapOpenAddress["cat"] = 5;
hashMapOpenAddress["dog"] = 5;
hashMapOpenAddress["cat"] = 10;
Console.WriteLine($"Contains cat: {hashMapOpenAddress.ContainsKey("cat")}");
Console.WriteLine($"cat = {hashMapOpenAddress["cat"]}");
hashMapOpenAddress["car"] = 15;
hashMapOpenAddress["yellow"] = 65;
hashMapOpenAddress["blue"] = 78;
Console.WriteLine($"yellow = {hashMapOpenAddress["yellow"]}");
Console.WriteLine($"blue = {hashMapOpenAddress["blue"]}");
hashMapOpenAddress.Remove("ddd");
hashMapOpenAddress.Remove("car");
Console.WriteLine($"Contains car: {hashMapOpenAddress.ContainsKey("car")}");
hashMapOpenAddress["cat2"] = 25;
hashMapOpenAddress["cat3"] = 35;
hashMapOpenAddress["cat4"] = 45;
hashMapOpenAddress["cat5"] = 55;
hashMapOpenAddress["cat6"] = 65;
hashMapOpenAddress["cat7"] = 75;
hashMapOpenAddress["cat8"] = 85;
hashMapOpenAddress["cat9"] = 95;
hashMapOpenAddress["cat9"] = 105;
Console.WriteLine($"cat = {hashMapOpenAddress["cat"]}");
Console.WriteLine($"yellow = {hashMapOpenAddress["yellow"]}");
Console.WriteLine($"blue = {hashMapOpenAddress["blue"]}");
Console.WriteLine($"Capacity: {hashMapOpenAddress.Capacity}");
Console.ReadKey();

Console.WriteLine("Хеш-таблица с квадратичным пробингом:");
var hashMapQuadraticProbe = new HashMapQuadraticProbe<string, int>(10);
Console.WriteLine($"Capacity: {hashMapQuadraticProbe.Capacity}");
hashMapQuadraticProbe["cat"] = 5;
hashMapQuadraticProbe["dog"] = 5;
hashMapQuadraticProbe["cat"] = 10;
Console.WriteLine($"Contains cat: {hashMapQuadraticProbe.ContainsKey("cat")}");
Console.WriteLine($"cat = {hashMapQuadraticProbe["cat"]}");
hashMapQuadraticProbe["car"] = 15;
hashMapQuadraticProbe["yellow"] = 65;
hashMapQuadraticProbe["blue"] = 78;
Console.WriteLine($"yellow = {hashMapQuadraticProbe["yellow"]}");
Console.WriteLine($"blue = {hashMapQuadraticProbe["blue"]}");
hashMapQuadraticProbe.Remove("ddd");
hashMapQuadraticProbe.Remove("car");
Console.WriteLine($"Contains car: {hashMapQuadraticProbe.ContainsKey("car")}");
hashMapQuadraticProbe["cat2"] = 25;
hashMapQuadraticProbe["cat3"] = 35;
hashMapQuadraticProbe["cat4"] = 45;
hashMapQuadraticProbe["cat5"] = 55;
hashMapQuadraticProbe["cat6"] = 65;
hashMapQuadraticProbe["cat7"] = 75;
hashMapQuadraticProbe["cat8"] = 85;
hashMapQuadraticProbe["cat9"] = 95;
hashMapQuadraticProbe["cat9"] = 105;
Console.WriteLine($"cat = {hashMapQuadraticProbe["cat"]}");
Console.WriteLine($"yellow = {hashMapQuadraticProbe["yellow"]}");
Console.WriteLine($"blue = {hashMapQuadraticProbe["blue"]}");
Console.WriteLine($"Capacity: {hashMapQuadraticProbe.Capacity}");
Console.ReadKey();