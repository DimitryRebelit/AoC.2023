/// <summary>
///     Generic interface for the advent of code solutions.
/// </summary>
public interface IPuzzle
{
    /// <summary>
    ///     Description of the puzzle
    /// </summary>
    string Description { get; }

    /// <summary>
    ///     Solve part one of the puzzle
    /// </summary>
    /// <returns></returns>
    Task SolvePartOneAsync();


    /// <summary>
    ///     Solve part two of the puzzle
    /// </summary>
    Task SolvePartTwoAsync();
}