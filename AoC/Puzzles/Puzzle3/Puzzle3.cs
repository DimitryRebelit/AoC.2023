using System.Text.RegularExpressions;
using AoC.Puzzles.Puzzle3.Models;

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

        var result = validPartNumbers.Select(x => int.Parse(x.Value)).Sum();
        Console.WriteLine($"Result: {result}");
    }

    /// <inheritdoc />
    protected override async Task SolvePartTwoAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(3, 2);
        var grid = GenerateGrid(input);
        var partNumbers = ExtractStringFromGrid(@"\d\w*", grid);
        var gearPositions = ExtractStringFromGrid(@"[^\d.]+", grid)
            .Where(x => x.Value == "*")
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
    private List<GridElement> ExtractValidPartNumbers(List<GridElement> partNumbers, List<GridElement> symbols)
    {
        const int Range = 1;
        var validPartNumbers = new List<GridElement>();
        foreach (var partNumber in partNumbers)
        {
            var coupledPartNumbers = symbols
                .Where(value =>
                    value.Row >= partNumber.Row - Range &&
                    value.Row <= partNumber.Row + Range &&
                    (value.StartColumn >= partNumber.StartColumn - Range && value.StartColumn <= partNumber.StartColumn + Range ||
                     value.StartColumn >= (partNumber.EndColumn) - Range &&
                     value.StartColumn <= (partNumber.EndColumn) + Range ))
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
    private List<GridElement> ExtractStringFromGrid(string regexPattern, char[][] grid)
    {
        var strings = new List<GridElement>();
        for (var row = 0; row < grid.Length; row++)
        {
            var match = Regex.Match(new string(grid[row]), regexPattern);
            while (match.Success)
            {
                strings.Add(new GridElement()
                {
                    Value = match.Value,
                    Row = row,
                    StartColumn = match.Index
                });
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
    private double GetGearRatio(List<GridElement> gearPositions, List<GridElement> partNumbers)
    {
        const int ColumnRange = 1;
        const int RowRange = 1;

        var gearRatio = new List<double>();
        foreach (var gearPosition in gearPositions)
        {
            var row = gearPosition.Row;
            var column = gearPosition.StartColumn;
            var coupledPartNumbers = partNumbers
                .Where(x =>
                    x.Row >= row - RowRange &&
                    x.Row <= row + RowRange &&
                    (x.StartColumn >= column - ColumnRange && x.StartColumn <= column + ColumnRange ||
                    x.EndColumn >= column - ColumnRange &&
                    x.EndColumn <= column + ColumnRange ))
                .ToList();

            if (coupledPartNumbers.Count == 2)
                gearRatio.Add(int.Parse(coupledPartNumbers[0].Value) * int.Parse(coupledPartNumbers[1].Value));
        }

        return gearRatio.Sum();
    }
}
