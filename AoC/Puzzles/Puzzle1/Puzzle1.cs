using System.Text.RegularExpressions;

namespace AoC.Puzzles.Puzzle1;

/// <summary>
///     Solution for Puzzle 1 of Advent of Code 2023.
/// </summary>
public class Puzzle1 : BasePuzzle
{
    private static readonly string[] s_numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    /// <inheritdoc />
    protected override string Title => "Day 1: Trebuchet?!";

    /// <inheritdoc />
    protected override int Day => 1;

    /// <inheritdoc />
    protected override async Task SolvePartOneAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(1,1);
        var result = input.Select(GetFirstAndLastNumber).ToList();
        Console.WriteLine($"Result: {result.Sum()}");
    }

    /// <inheritdoc />
    protected override async Task SolvePartTwoAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(1,2);
        var result = input
            .Select(ExtractNumbersFromString)
            .Select(GetFirstAndLastNumber)
            .ToList();

        Console.WriteLine($"Result: {result.Sum()}");
    }

    /// <summary>
    ///     Replace number words with digits
    /// </summary>
    /// <param name="line"></param>
    /// <remarks>
    ///    Replaces the words one till nine with digits
    /// </remarks>
    /// <returns></returns>
    private string ExtractNumbersFromString(string line)
    {
        var replacedLine = string.Empty;
        var pattern = @"(?=(\d|one|two|three|four|five|six|seven|eight|nine))";
        var numberWordMatch = Regex.Match(line, pattern);

        while (numberWordMatch.Success)
        {
            var matchedValue = numberWordMatch.Groups[1].Value;
            var number = char.IsLetter(matchedValue[0]) ?
                (Array.IndexOf(s_numbers, matchedValue) + 1).ToString() :
                matchedValue;

            replacedLine += number;
            numberWordMatch = numberWordMatch.NextMatch();
        }

        return replacedLine;
    }

    /// <summary>
    ///     Get the first and last number from a string
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    private int GetFirstAndLastNumber(string line)
    {
        Regex pattern = new (@"^\D*(\d).*\D*(\d)\D*$");
        var match = pattern.Match(line);
        if (match.Groups.Count > 2)
            return int.Parse($"{match.Groups[1]}{match.Groups[2]}");

        pattern = new (@"\d");
        match = pattern.Match(line);
        return int.Parse($"{match.Groups[0]}{match.Groups[0]}");
    }
}
