namespace Simulator;

public abstract class Creature
{
    private string _name;
    private int _level;

    public string Name
    {
        get => _name;
        init => _name = ValidationAndFormatingName(value);
    }

    public int Level
    {
        get => _level;
        init => _level = ValidateLevel(value);
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
    private string ValidationAndFormatingName(string inputName)
    {
        string newName = (inputName ?? "").Trim();

        if (newName.Length > 25)
        {
            newName = newName.Substring(0, 25).TrimEnd();
        }

        if (newName.Length < 3)
        {
            newName = newName.PadRight(3, '#');
        }

        if (newName.Length > 0 && char.IsLower(newName[0]))
        {
            newName = char.ToUpper(newName[0]) + newName.Substring(1);
        }

        return newName;

    }

    public int ValidateLevel(int inputLevel)
    {
        if (inputLevel < 1) return 1;
        if (inputLevel > 10) return 10;
        return inputLevel;
    }

    public void Go(Direction direction)
    {
        string lowerDirection = direction.ToString().ToLower();

        Console.WriteLine($"{Name} goes {lowerDirection}.");
    }

    public void Go(Direction[] directions)
    {
        foreach (Direction direction in directions)
        {
            Go(direction);
        }
    }

    public void Go(string directionsString)
    {
        Direction[] directions = DirectionParser.Parse(directionsString);

        Go(directions);
    }

    public abstract void SayHi();
    

    public void Upgrade()
    {
        if (Level < 10)
        {
            _level += 1;
        }
    }

    public virtual string Info
    {
        get { return $"{Name} (level {Level})"; }
    }

    public abstract int Power { get; }
}
