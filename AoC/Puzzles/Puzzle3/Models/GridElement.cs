// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace AoC.Puzzles.Puzzle3.Models;

/// <summary>
///     Element in the grid
/// </summary>
public class GridElement
{
    /// <summary>
    ///     Value of the element in the grid
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    ///     Row of the element in the grid
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    ///     Start column of the element in the grid
    /// </summary>
    public int StartColumn { get; set; }

    /// <summary>
    ///     End column of the element in the grid
    /// </summary>
    public int EndColumn => StartColumn + Value.Length - 1;
}
