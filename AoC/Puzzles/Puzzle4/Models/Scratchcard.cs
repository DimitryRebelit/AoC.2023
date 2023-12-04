// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace AoC.Puzzles.Puzzle4.Models;

public class Scratchcard
{
    public int Id { get; set; }
    public List<int> WinningNumbers { get; set; }
    public List<int> MyNumbers { get; set; }
    public int Copies { get; set; } = 1;
}
