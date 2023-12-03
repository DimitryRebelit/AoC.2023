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
        var input = await base.ReadAllLinesFromInputAsync(3, 1, true);
        var grid = GenerateGrid(input);
        var partNumbers = GetPartNumbers(grid);
        var symbols = GetSymbolPositions(grid);
        var validPartNumbers = FilterValidPartNumbers(partNumbers, symbols);

        var result = validPartNumbers.Select(x => int.Parse(x.Number)).Sum();
        Console.WriteLine($"Result: {result}");
    }

    /// <inheritdoc />
    protected override async Task SolvePartTwoAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(3, 1 );
        var grid = GenerateGrid(input);
        var gearPositions = GetSymbolPositions(grid).Where(x => x.Item1 == '*').ToList();
        var partNumbers = GetPartNumbers(grid);

        var result = GetGearRatio(gearPositions, partNumbers);
        Console.WriteLine("Result: " + result);
    }

    /// <summary>
    ///     Generate matrix grid
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
    private List<PartNumber> FilterValidPartNumbers(List<PartNumber> partNumbers, List<(char, int, int)> symbols)
    {
        const int Range = 1;
        var validPartNumbers = new List<PartNumber>();
        foreach (var partNumber in partNumbers)
        {
            var coupledPartNumbers = symbols
                .Where(value =>
                    value.Item2 >= partNumber.Row - Range &&
                    value.Item2 <= partNumber.Row + Range &&
                    (value.Item3 >= partNumber.StartIndex - Range && value.Item3 <= partNumber.StartIndex + Range ||
                     value.Item3 >= partNumber.EndIndex - Range && value.Item3 <= partNumber.EndIndex + Range ))
                .ToList();

            if(coupledPartNumbers.Count != 0)
                validPartNumbers.Add(partNumber);
        }

        return validPartNumbers;
    }

    /// <summary>
    ///     Get all part numbers from the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    private List<PartNumber> GetPartNumbers(char[][] grid)
    {
        const string RegexPattern = @"\d\w*";

        var partNumbers = new List<PartNumber>();
        for (var row = 0; row < grid.Length; row++)
        {
            var match = Regex.Match(new string(grid[row]), RegexPattern);
            while (match.Success)
            {
                partNumbers.Add(new PartNumber()
                {
                    Row = row,
                    StartIndex = match.Index,
                    Number = match.Value,
                    EndIndex = match.Index + (match.Length - 1)
                });
                match = match.NextMatch();
            }
        }

        return partNumbers;
    }

    /// <summary>
    ///     Get the gear ratio from the grid
    /// </summary>
    /// <param name="gearPositions"></param>
    /// <param name="partNumbers"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    private double GetGearRatio(List<(char,int, int)> gearPositions, List<PartNumber> partNumbers)
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
                    x.Row >= row - RowRange &&
                    x.Row <= row + RowRange &&
                    (x.StartIndex >= column - ColumnRange && x.StartIndex <= column + ColumnRange ||
                    x.EndIndex >= column - ColumnRange && x.EndIndex <= column + ColumnRange ))
                .ToList();

            if (coupledPartNumbers.Count == 2)
                gearRatio.Add(int.Parse(coupledPartNumbers[0].Number) * int.Parse(coupledPartNumbers[1].Number));
        }

        return gearRatio.Sum();
    }

    /// <summary>
    ///     Get all symbols from the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    private List<(char, int, int)> GetSymbolPositions(char[][] grid)
    {
        const string RegexPattern = @"[^\d.]+";
        var symbols = new List<(char, int, int)>();
        for (var row = 0; row < grid.Length; row++)
        {
            var match = Regex.Match(new string(grid[row]), RegexPattern);
            while (match.Success)
            {
                symbols.Add((match.Value[0], row, match.Index));
                match = match.NextMatch();
            }
        }
        return symbols;
    }
}
