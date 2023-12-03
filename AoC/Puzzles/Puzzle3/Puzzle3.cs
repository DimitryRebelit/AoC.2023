using System.Text.RegularExpressions;
using AoC.Puzzles.Models;

namespace AoC.Puzzles;

public class Puzzle3 : BasePuzzle
{
    /// <inheritdoc />
    protected override string Title => "Day 3: Gear Ratios";

    /// <inheritdoc />
    protected override async Task SolvePartOneAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(3, 1);
        var grid = GenerateGrid(input);
        var partNumbers = GetPartNumbers(grid);
        var validPartNumbers = FilterValidPartNumbers(partNumbers, grid);

        var result = validPartNumbers.Select(x => int.Parse(x.Number)).Sum();
        Console.WriteLine($"Result: {result}");
    }

    /// <inheritdoc />
    protected override async Task SolvePartTwoAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(3, 1 );
        var grid = GenerateGrid(input);
        var gearPositions = GetGearPositions(grid);
        var partNumbers = GetPartNumbers(grid);

        var result = GetGearRatio(gearPositions, partNumbers, grid);
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
    ///     Filter out all part numbers that are not around a gear symbol
    /// </summary>
    /// <param name="partNumbers"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    private List<PartNumber> FilterValidPartNumbers(List<PartNumber> partNumbers, char[][] grid)
    {
        var validPartNumbers = new List<PartNumber>();
        foreach (var partNumber in partNumbers)
        {
            var isValid = false;
            for (var row = partNumber.Row - 1; row <= partNumber.Row + 1; row++)
            {
                for (var column = partNumber.StartIndex - 1; column <= partNumber.EndIndex + 1; column++)
                {
                    if (row < 0 || row >= grid.Length || column < 0 || column >= grid[row].Length)
                        continue;

                    var character = grid[row][column];
                    if (char.IsDigit(character) || character == '.')
                        continue;

                    isValid = true;
                    break;
                }

                if (!isValid)
                    continue;

                validPartNumbers.Add(partNumber);
                break;
            }
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
    private double GetGearRatio(List<(int, int)> gearPositions, List<PartNumber> partNumbers, char[][] grid)
    {
        const int ColumnRange = 1;
        const int RowRange = 1;

        var gearRatio = new List<double>();
        foreach (var gearPosition in gearPositions)
        {
            var row = gearPosition.Item1;
            var column = gearPosition.Item2;
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
    ///     Get all gear positions from the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    private List<(int, int)> GetGearPositions(char[][] grid)
    {
        // Find all gear positions, that are the * symbols in the grid
        var gearPositions = new List<(int, int)>();
        for (var row = 0; row < grid.Length; row++)
        {
            for (var column = 0; column < grid[row].Length; column++)
            {
                if (grid[row][column] == '*')
                {
                    gearPositions.Add((row, column));
                }
            }
        }

        return gearPositions;
    }
}
