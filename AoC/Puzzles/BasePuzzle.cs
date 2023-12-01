/// <summary>
///    Base class for the advent of code solutions.
/// </summary>
/// <remarks>
///    Contains basic logic for loading the input file and solving the puzzle.
/// </remarks>
public abstract class BasePuzzle
{
    /// <summary>
    ///     Open the input file for the puzzle.
    /// </summary>
    /// <param name="puzzle">Number of the puzzle</param>
    /// <param name="isSample">Whether to load the sample input file</param>
    /// <remarks>
    ///     The input file is expected to be in the Resources folder and named AoC.Puzzles.{puzzle}.txt
    /// </remarks>
    /// <returns></returns>
    public virtual async Task<List<string>> ReadAllLinesFromInputAsync(int puzzle, int part, bool isSample = false)
    {
        var resourcePath = "Resources/";
        var resourceName = isSample ? $"AoC.Puzzles.{puzzle}.{part}.sample.txt" : $"AoC.Puzzles.{puzzle}.{part}.txt";
        var path = $"{resourcePath}{resourceName}";

        if (!File.Exists(path))
            throw new FileNotFoundException($"Input file not found: {path}");

        using var stream = new StreamReader(path);
        var result = new List<string>();
        while (!stream.EndOfStream)
        {
            var line = await stream.ReadLineAsync();
            result.Add(string.IsNullOrWhiteSpace(line) ? string.Empty : line);
        }
        return result;
    }
}
