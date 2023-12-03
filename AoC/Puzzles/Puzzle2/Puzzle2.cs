using System.Text.RegularExpressions;

namespace AoC.Puzzles.Puzzle2;

/// <inheritdoc cref="BasePuzzle"/>
public class Puzzle2 : BasePuzzle
{
    /// <inheritdoc cref="BasePuzzle"/>
    protected override string Title => "Day 2: Cube Conundrum";

    /// <inheritdoc cref="BasePuzzle"/>
    protected override int Day => 2;

    /// <inheritdoc cref="BasePuzzle"/>
    protected override async Task SolvePartOneAsync()
    {
        var possibleGames = new List<int>();
        var input = await base.ReadAllLinesFromInputAsync(2, 1);

        foreach (var line in input)
        {
            var gameId = Regex.Match(line.Split(":")[0], @"\b\d+\b").Value;
            var bag = GetBagContent(line);
            if (bag["red"] <= 12 && bag["green"] <= 13 && bag["blue"] <= 14)
                possibleGames.Add(int.Parse(gameId));
        }

        Console.WriteLine($"Result: {possibleGames.Sum()}");
    }

    /// <inheritdoc cref="BasePuzzle"/>
    protected override async Task SolvePartTwoAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(2, 2);
        var power = input
            .Select(GetBagContent)
            .Select(bag => bag["red"] * bag["green"] * bag["blue"])
            .ToList();

        Console.WriteLine($"Result: {power.Sum()}");
    }

    /// <summary>
    ///     Get the content of a bag
    /// </summary>
    /// <param name="input"></param>
    /// <returns>
    ///    A dictionary containing the amount of red, green and blue cubes in the bag
    /// </returns>
    private static Dictionary<string, int> GetBagContent(string input)
    {
        var bag = new Dictionary<string, int>();
        var sets = input
            .Split(":")[1]
            .Split(";")
            .SelectMany(x => x.Split(","))
            .ToList();

        foreach (var set in sets)
        {
            var amount = Regex.Match(set, @"\b\d+\b").Value;
            var colour = Regex.Match(set, "[a-z]+").Value;

            if (bag.TryGetValue(colour, out var value))
            {
                if(value < int.Parse(amount))
                    bag[colour] = int.Parse(amount);
            }
            else
            {
                bag.Add(colour, int.Parse(amount));
            }
        }
        return bag;
    }
}
