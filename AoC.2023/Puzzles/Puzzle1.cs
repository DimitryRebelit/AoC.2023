namespace AoC.2023.Puzzles;

/// <summary>
///     Solution for Puzzle 1 of Advent of Code 2023.
/// </summary>
public class Puzzle1 : BasePuzzle, IPuzzle
{
    /// <inheritdoc />
    public string Description => "Day 1: Report Repair";

    /// <inheritdoc />
    public async Task SolvePartOneAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(1, true);
        input.ForEach(x => Console.WriteLine(x));
    }

    /// <inheritdoc />
    public Task SolvePartTwoAsync()
    {
        throw new NotImplementedException();
    }
}