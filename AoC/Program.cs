using AoC.Puzzles;
using AoC.Puzzles.Puzzle1;
using AoC.Puzzles.Puzzle2;
using AoC.Puzzles.Puzzle3;
using AoC.Puzzles.Puzzle4;

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

var solvedPuzzles = new List<BasePuzzle>()
{
    new Puzzle1(),
    new Puzzle2(),
    new Puzzle3(),
    new Puzzle4()
};

foreach (var puzzle in solvedPuzzles)
{
    await puzzle.SolveAsync();
}

Console.ReadKey();
