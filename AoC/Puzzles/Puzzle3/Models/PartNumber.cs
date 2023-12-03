// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace AoC.Puzzles.Puzzle3.Models;

public class PartNumber
{
    public int Row { get; set; }
    public int StartIndex { get; set; }
    public int? EndIndex { get; set; }
    public string Number { get; set; }
}
