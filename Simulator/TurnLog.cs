using System.Collections.Generic;

namespace Simulator;

/// <summary>
/// State of map after single simulation turn.
/// </summary>
public class TurnLog
{
    /// <summary>
    /// Text representation of moving object in this turn.
    /// CurrentMappable.ToString()
    /// </summary>
    public required string Mappable { get; init; }

    /// <summary>
    /// Text representation of move in this turn.
    /// CurrentMoveName.ToString();
    /// </summary>
    public required string Move { get; init; }

    /// <summary>
    /// Dictionary of map symbols in this turn.
    /// Keys: occupied points only (no empty fields).
    /// Values: single symbol or 'X' when multiple objects are on one field.
    /// </summary>
    public required Dictionary<Point, char> Symbols { get; init; }
}
