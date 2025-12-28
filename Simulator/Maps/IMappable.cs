namespace Simulator.Maps;

/// <summary>
/// Contract for objects that can be placed on a <see cref="Map"/>.
/// This decouples maps and simulations from concrete types like <c>Creature</c>.
/// </summary>
public interface IMappable
{
    /// <summary>
    /// Display name (used e.g. by console UI).
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Symbol used by map visualizers.
    /// </summary>
    char Symbol { get; }

    /// <summary>
    /// Map on which the object currently is (or null if not placed).
    /// </summary>
    Map? CurrentMap { get; }

    /// <summary>
    /// Current position on the map (or null if not placed).
    /// </summary>
    Point? Position { get; }

    /// <summary>
    /// Perform a move in given direction.
    /// </summary>
    void Go(Direction direction);

    /// <summary>
    /// Called by <see cref="Map"/> to keep object state in sync.
    /// </summary>
    void SetLocation(Map map, Point position);

    /// <summary>
    /// Called by <see cref="Map"/> when object is removed.
    /// </summary>
    void ClearLocation();
}
