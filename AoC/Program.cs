// See https://aka.ms/new-console-template for more information.
Console.WriteLine("Hello, World!");

var puzzle1 = new Puzzle1();

Console.WriteLine(puzzle1.Description);
await puzzle1.SolvePartOneAsync();
await puzzle1.SolvePartTwoAsync();
Console.ReadKey();
