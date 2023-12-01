using System.Text.RegularExpressions;

/// <summary>
///     Solution for Puzzle 1 of Advent of Code 2023.
/// </summary>
public class Puzzle1 : BasePuzzle, IPuzzle
{
    /// <inheritdoc />
    public string Description => "--- Day 1: Trebuchet?! ---";
    private readonly Regex _firstLastRegex = new (@"^\D*(\d).*\D*(\d)\D*$");
    private readonly Regex _single = new (@"\d");
    private readonly string _numberPattern = @"(?=(\d|one|two|three|four|five|six|seven|eight|nine))";

    /// <inheritdoc />
    public async Task SolvePartOneAsync()
    {
        var result = new List<int>();
        var input = await base.ReadAllLinesFromInputAsync(1,1);
        foreach (var line in input)
        {
            var matches = _firstLastRegex.Match(line);
            if(matches.Groups.Count > 2)
                result.Add(int.Parse($"{matches.Groups[1]}{matches.Groups[2]}"));
            else
            {
                var match = _single.Match(line);
                result.Add(int.Parse($"{match.Groups[0]}{match.Groups[0]}"));
            }
        }
        Console.WriteLine($"Result: {result.Sum()}");
    }

    /// <inheritdoc />
    public async Task SolvePartTwoAsync()
    {
        var result = new List<int>();

        var input = await base.ReadAllLinesFromInputAsync(1,2);
        foreach (var line in input)
        {
            var replacedLine = "";
            var numberWordMatch = Regex.Match(line, _numberPattern);

            while (numberWordMatch.Success)
            {
                var matchedValue = numberWordMatch.Groups[1].Value;
                var number = char.IsLetter(matchedValue[0]) ?
                    (Array.IndexOf(new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }, matchedValue) + 1).ToString() :
                    matchedValue;

                replacedLine += number;
                numberWordMatch = numberWordMatch.NextMatch();
            }

            var matches = _firstLastRegex.Match(replacedLine);
            if (matches.Groups.Count > 2)
            {
                result.Add(int.Parse($"{matches.Groups[1]}{matches.Groups[2]}"));
            }
            else
            {
                var match = _single.Match(replacedLine);
                Console.WriteLine($"{match.Groups[0]}{match.Groups[0]}");
                result.Add(int.Parse($"{match.Groups[0]}{match.Groups[0]}"));
            }
        }

        Console.WriteLine($"Result: {result.Sum()}");
    }
}
