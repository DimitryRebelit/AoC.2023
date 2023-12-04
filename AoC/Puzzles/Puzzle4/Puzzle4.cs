using System.Text.RegularExpressions;
using AoC.Puzzles.Puzzle4.Models;

namespace AoC.Puzzles.Puzzle4;

public class Puzzle4 : BasePuzzle
{
    /// <inheritdoc />
    protected override string Title => "Day 4: Scratchcards";

    /// <inheritdoc />
    protected override int Day => 4;

    /// <inheritdoc />
    protected override async Task SolvePartOneAsync()
    {
        var totalWorth = 0d;
        var input = await base.ReadAllLinesFromInputAsync(Day, 1);
        var scratchcards = GetScratchcards(input);
        foreach (var scratchcard in scratchcards)
        {
            var matchingNumbers = scratchcard.MyNumbers.Intersect(scratchcard.WinningNumbers).Count();
            if(matchingNumbers > 0)
                totalWorth += Math.Pow(2, matchingNumbers - 1);
        }

        Console.WriteLine($"Result: {totalWorth}");
    }

    /// <inheritdoc />
    protected override async Task SolvePartTwoAsync()
    {
        var input = await base.ReadAllLinesFromInputAsync(Day, 2);
        var scratchcards = GetScratchcards(input);
        foreach (var scratchCard in scratchcards)
        {
            var matchingNumbers = scratchCard.MyNumbers.Intersect(scratchCard.WinningNumbers).ToList();
            for (var cardIndex = 1; cardIndex <= scratchCard.Copies; cardIndex++)
            for (var i = 1; i <= matchingNumbers.Count; i++)
            {
                scratchcards.First(x => x.Id == scratchCard.Id + i).Copies += 1;
            }
        }

        var totalWorth = scratchcards.Sum(x => x.Copies);
        Console.WriteLine($"Result: {totalWorth}");
    }

    /// <summary>
    ///     Get the scratchcards from the input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private List<Scratchcard> GetScratchcards(List<string> input)
    {
        var scratchcards = new List<Scratchcard>();
        foreach (var line in input)
        {
            var id = int.Parse(Regex.Match(line.Split(":")[0], @"\d+").Value);
            var card = line.Split(":")[1];
            var winningNumbers = Regex.Matches(card.Split("|")[0], @"\d+")
                .Select(x => int.Parse(x.Value))
                .ToList();

            var myNumbers = Regex.Matches(card.Split("|")[1], @"\d+")
                .Select(x => int.Parse(x.Value))
                .ToList();

            scratchcards.Add(new Scratchcard()
            {
                Id = id,
                WinningNumbers = winningNumbers,
                MyNumbers = myNumbers
            });
        }
        return scratchcards;
    }
}
