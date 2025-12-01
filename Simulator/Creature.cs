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

    public string Go(Direction direction) => $"{direction.ToString().ToLower()}";

    public string[] Go(Direction[] directions)
    {
        if (directions == null)
        {
            return new string[0];
        }

        var results = new string[directions.Length];

        for (int i = 0; i < directions.Length; i++)
        {
            results[i] = Go(directions[i]);
        }

        return results;
    }

    public string[] Go(string directionsString)
    {
        Direction[] directions = DirectionParser.Parse(directionsString);
        return Go(directions);
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

    public override string ToString()
    {
        var typeName = GetType().Name.ToUpperInvariant();
        return $"{typeName}: {Info}";
    }
}
