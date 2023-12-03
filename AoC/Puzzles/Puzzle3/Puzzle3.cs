using System.Text.RegularExpressions;

namespace AoC.Puzzles.Puzzle3;

public class Puzzle3 : BasePuzzle
{
    /// <inheritdoc />
    protected override string Title => "Day 3: Gear Ratios";

    /// <inheritdoc />
    protected override int Day => 3;

    /// <inheritdoc />
    protected override async Task SolvePartOneAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(3, 1);
        var grid = GenerateGrid(input);
        var partNumbers = ExtractStringFromGrid(@"\d\w*", grid);
        var symbols = ExtractStringFromGrid(@"[^\d.]+", grid);
        var validPartNumbers = ExtractValidPartNumbers(partNumbers, symbols);

        var result = validPartNumbers.Select(x => int.Parse(x.Item1)).Sum();
        Console.WriteLine($"Result: {result}");
    }

    /// <inheritdoc />
    protected override async Task SolvePartTwoAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(3, 2);
        var grid = GenerateGrid(input);
        var partNumbers = ExtractStringFromGrid(@"\d\w*", grid);
        var gearPositions = ExtractStringFromGrid(@"[^\d.]+", grid)
            .Where(x => x.Item1 == "*")
            .ToList();

        var result = GetGearRatio(gearPositions, partNumbers);
        Console.WriteLine($"Result: {result}");
    }

    /// <summary>
    ///     Generate matrix grid based on the input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static char[][] GenerateGrid(List<string> input)
    {
        var grid = new char[input.Count][];
        for (var i = 0; i < input.Count; i++)
        {
            grid[i] = input[i].ToCharArray();
        }
        return grid;
    }

    /// <summary>
    ///     Filter out all part numbers that are not around a symbol
    /// </summary>
    /// <param name="partNumbers"></param>
    /// <param name="symbols"></param>
    /// <returns></returns>
    private List<(string,int,int)> ExtractValidPartNumbers(List<(string,int,int)> partNumbers, List<(string, int, int)> symbols)
    {
        const int Range = 1;
        var validPartNumbers = new List<(string,int,int)>();
        foreach (var partNumber in partNumbers)
        {
            var coupledPartNumbers = symbols
                .Where(value =>
                    value.Item2 >= partNumber.Item2 - Range &&
                    value.Item2 <= partNumber.Item2 + Range &&
                    (value.Item3 >= partNumber.Item3 - Range && value.Item3 <= partNumber.Item3 + Range ||
                     value.Item3 >= (partNumber.Item3 + (partNumber.Item1.Length - 1)) - Range &&
                     value.Item3 <= (partNumber.Item3 + (partNumber.Item1.Length - 1)) + Range ))
                .ToList();

            if(coupledPartNumbers.Count != 0)
                validPartNumbers.Add(partNumber);
        }

        return validPartNumbers;
    }

    /// <summary>
    ///     Extract strings from the grid
    /// </summary>
    /// <param name="regexPattern"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    private List<(string, int, int)> ExtractStringFromGrid(string regexPattern, char[][] grid)
    {
        var strings = new List<(string, int, int)>();
        for (var row = 0; row < grid.Length; row++)
        {
            var match = Regex.Match(new string(grid[row]), regexPattern);
            while (match.Success)
            {
                strings.Add((match.Value, row, match.Index));
                match = match.NextMatch();
            }
        }
        return strings;
    }

    /// <summary>
    ///     Get the gear ratio from the grid
    /// </summary>
    /// <param name="gearPositions"></param>
    /// <param name="partNumbers"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    private double GetGearRatio(List<(string,int, int)> gearPositions, List<(string,int, int)> partNumbers)
    {
        const int ColumnRange = 1;
        const int RowRange = 1;

        var gearRatio = new List<double>();
        foreach (var gearPosition in gearPositions)
        {
            var row = gearPosition.Item2;
            var column = gearPosition.Item3;
            var coupledPartNumbers = partNumbers
                .Where(x =>
                    x.Item2 >= row - RowRange &&
                    x.Item2 <= row + RowRange &&
                    (x.Item3 >= column - ColumnRange && x.Item3 <= column + ColumnRange ||
                    x.Item3 + x.Item1.Length - 1 >= column - ColumnRange &&
                    x.Item3 + x.Item1.Length - 1 <= column + ColumnRange ))
                .ToList();

            if (coupledPartNumbers.Count == 2)
                gearRatio.Add(int.Parse(coupledPartNumbers[0].Item1) * int.Parse(coupledPartNumbers[1].Item1));
        }

        return gearRatio.Sum();
    }
}
