using Simulator.Maps;

namespace Simulator;

public abstract class Creature
{
    private string _name;
    private int _level;

    public string Name
    {
        get => _name;
        init => _name = Validator.Shortener(value, 3, 25, '#');
    }

    public int Level
    {
        get => _level;
        init => _level = Validator.Limiter(value, 1, 10);
    }

    public Creature()
    {
        Name = "Uknown";
        Level = 1;
    }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    /// <summary>
    /// Map on which the creature currently is. May be null until assigned.
    /// </summary>
    public Map? CurrentMap { get; private set; }

    /// <summary>
    /// Current position of the creature on the map. Null if creature has no map.
    /// </summary>
    public Point? Position { get; private set; }

    /// <summary>
    /// Internal helper used by Map to place creature on a map.
    /// </summary>
    internal void SetLocation(Map map, Point position)
    {
        CurrentMap = map;
        Position = position;
    }

    /// <summary>
    /// Internal helper used by Map to remove creature from a map.
    /// </summary>
    internal void ClearLocation()
    {
        CurrentMap = null;
        Position = null;
    }

    public void Go(Direction direction)
    {
        if (CurrentMap is null || Position is null)
        {
            // Creature without a map or starting position cannot move.
            return;
        }

        var from = Position.Value;
        var to = CurrentMap.Next(from, direction);

        // If map decides that we stay in place (e.g. border), skip move.
        if (to.Equals(from))
        {
            return;
        }

        CurrentMap.Move(this, from, to);
    }

    public abstract string Greeting();

    public void Upgrade()
    {
        if (Level < 10)
        {
            _level += 1;
        }
    }

    public abstract string Info { get; }
    public abstract int Power { get; }

    public abstract char Symbol { get; }


    public override string ToString()
    {
        var typeName = GetType().Name.ToUpperInvariant();
        return $"{typeName}: {Info}";
    }
}
