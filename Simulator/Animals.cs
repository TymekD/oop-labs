namespace Simulator;

using Simulator.Maps;

public class Animals : IMappable
{
    private string _description = "Unknown";

    public string Description
    {
        get => _description;
        init => _description = Validator.Shortener(value, 3, 15, '#');
    }

    /// <summary>
    /// Number of animals in herd/group.
    /// </summary>
    public uint Size { get; set; } = 3;

    // --- IMappable state ---
    public Map? CurrentMap { get; private set; }
    public Point? Position { get; private set; }

    // --- IMappable identity ---
    public string Name => Description;

    /// <summary>
    /// Default symbol for animals (non-birds).
    /// Birds override this in Birds class.
    /// </summary>
    public virtual char Symbol => 'A';

    public Animals()
    {
    }

    public Animals(string description)
    {
        Description = description;
    }

    public virtual string Info => $"{Description} <{Size}>";

    /// <summary>
    /// Default movement: 1 step using map.Next().
    /// Birds override this with special rules.
    /// </summary>
    public virtual void Go(Direction direction)
    {
        if (CurrentMap is null || Position is null)
            return;

        Point from = Position.Value;
        Point to = CurrentMap.Next(from, direction);

        CurrentMap.Move(this, from, to);
    }

    // Map keeps object state in sync via IMappable
    public void SetLocation(Map map, Point position)
    {
        CurrentMap = map;
        Position = position;
    }

    public void ClearLocation()
    {
        CurrentMap = null;
        Position = null;
    }

    public override string ToString()
    {
        var typeName = GetType().Name.ToUpperInvariant();
        return $"{typeName}: {Info}";
    }
}
