Console.WriteLine("         |");
Console.WriteLine("        -+-");
Console.WriteLine("         A");
Console.WriteLine("        /=\\               /\\  /\\    ___  _ __  _ __ __    __");
Console.WriteLine("      i/ O \\i            /  \\/  \\  / _ \\| '__|| '__|\\ \\  / /");
Console.WriteLine("      /=====\\           / /\\  /\\ \\|  __/| |   | |    \\ \\/ /");
Console.WriteLine("      /  i  \\           \\ \\ \\/ / / \\___/|_|   |_|     \\  /");
Console.WriteLine("    i/ O * O \\i                                       / /");
Console.WriteLine("    /=========\\        __  __                        /_/    _");
Console.WriteLine("    /  *   *  \\        \\ \\/ /        /\\  /\\    __ _  ____  | |");
Console.WriteLine("  i/ O   i   O \\i       \\  /   __   /  \\/  \\  / _` |/ ___\\ |_|");
Console.WriteLine("  /===========\\       / \\  |__| / /\\  /\\ \\| (_| |\\___ \\  _");
Console.WriteLine("  /  O   i   O  \\      /_/\\_\\      \\ \\ \\/ / / \\__,_|\\____/ |_|");
Console.WriteLine("i/ *   O   O   * \\i");
Console.WriteLine("/=================\\");
Console.WriteLine("       |___|");
Console.WriteLine("");
Console.WriteLine("");

var solvedPuzzles = new List<IPuzzle>() {new Puzzle1()};

foreach (var puzzle in solvedPuzzles)
{
    Console.WriteLine(puzzle.Description);
    await puzzle.SolvePartOneAsync();
    await puzzle.SolvePartTwoAsync();
}

Console.ReadKey();
