namespace Simulator.Maps;

/// <summary>
/// Base class for all maps.
/// All maps are rectangular with immutable dimensions (SizeX, SizeY)
/// and coordinates ranging from (0, 0) to (SizeX - 1, SizeY - 1).
/// It also keeps track of creatures standing on its fields.
/// </summary>
public abstract class Map
{
    private const int MinSize = 5;

    /// <summary>
    /// Map width (number of columns).
    /// </summary>
    public int SizeX { get; }

    /// <summary>
    /// Map height (number of rows).
    /// </summary>
    public int SizeY { get; }

    /// <summary>
    /// Rectangle describing map bounds from (0,0) to (SizeX-1, SizeY-1).
    /// </summary>
    protected Rectangle Bounds { get; }

    private readonly Dictionary<Point, List<Creature>> _creatures =
        new();

    protected Map(int sizeX, int sizeY)
    {
        if (sizeX < MinSize)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sizeX),
                $"Map width (SizeX) must be at least {MinSize}."
            );
        }

        if (sizeY < MinSize)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sizeY),
                $"Map height (SizeY) must be at least {MinSize}."
            );
        }

        SizeX = sizeX;
        SizeY = sizeY;
        Bounds = new Rectangle(0, 0, SizeX - 1, SizeY - 1);
    }

    /// <summary>
    /// Check if give point belongs to the map.
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns></returns>
    public virtual bool Exist(Point p) => Bounds.Contains(p);

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point NextDiagonal(Point p, Direction d);

    /// <summary>
    /// Adds creature to a given point on this map.
    /// Also updates creature's CurrentMap and Position.
    /// </summary>
    public void Add(Creature creature, Point position)
    {
        if(creature is null)
            throw new ArgumentNullException(nameof(creature));

        if (!Exist(position))
            throw new ArgumentOutOfRangeException(
                nameof(position),
                "Position must be on the map!"
            );

        if (creature.CurrentMap != null && creature.CurrentMap != this)
        {
            throw new InvalidOperationException(
                "Creature already belongs to a different map."
            );
        }

        // If the creature is already on this map, remove it from previous position.
        if (creature.CurrentMap == this && creature.Position is Point oldPos)
        {
            InternalRemove(creature, oldPos);
        }

        InternalAdd(creature, position);
        creature.SetLocation(this, position);
    }

    /// <summary>
    /// Removes creature from this map (if present).
    /// </summary>
    public void Remove(Creature creature)
    {
        if (creature is null)
            throw new ArgumentNullException(nameof(creature));

        if (creature.CurrentMap != this || creature.Position is null)
            return;

        InternalRemove(creature, creature.Position.Value);
        creature.ClearLocation();
    }

    /// <summary>
    /// Moves creature from one point to another.
    /// Used by Creature.Go to keep map and creature state in sync.
    /// </summary>
    public void Move(Creature creature, Point from, Point to)
    {
        if (creature is null)
            throw new ArgumentNullException(nameof(creature));

        if (!Exist(to))
            throw new ArgumentOutOfRangeException(
                nameof(to),
                "Destination point must belong to the map."
            );

        InternalRemove(creature, from);
        InternalAdd(creature, to);
        creature.SetLocation(this, to);
    }

    private void InternalAdd(Creature creature, Point position)
    {
        if (!_creatures.TryGetValue(position, out var list))
        {
            list = new List<Creature>();
            _creatures[position] = list;
        }

        if (!list.Contains(creature))
        {
            list.Add(creature);
        }
    }

    private void InternalRemove(Creature creature, Point position)
    {
        if (_creatures.TryGetValue(position, out var list))
        {
            list.Remove(creature);
            if (list.Count == 0)
            {
                _creatures.Remove(position);
            }
        }
    }

    /// <summary>
    /// Returns creatures present at given point. Empty array if there are none.
    /// </summary>
    public IReadOnlyList<Creature> At(Point position)
    {
        if (!Exist(position))
            throw new ArgumentOutOfRangeException(
                nameof(position),
                "Position must belong to the map."
            );

        if (_creatures.TryGetValue(position, out var list))
        {
            return list;
        }

        return Array.Empty<Creature>();
    }

    /// <summary>
    /// Overload of At using coordinates instead of Point struct.
    /// </summary>
    public IReadOnlyList<Creature> At(int x, int y) => At(new Point(x, y));
}
