namespace Simulator.Creatures;

public class Elf : Creature
{
    private int _agility;
    private int _singCount;
    public int Agility 
    { 
        get => _agility;
        init => _agility = StatLimit(value);
    }

    public Elf()
    {
        _agility = StatLimit(1);
    }

    public Elf(string name, int level = 1, int agility = 1) : base(name, level)
    {
           _agility = StatLimit(agility);
    }


    public void Sing()
    {         
        _singCount++;
        if (_singCount % 3 == 0)
        {
            _agility = StatLimit(_agility + 1);
        }
        Console.WriteLine($"{Name} is singing!");
    }

    public override void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}, agility {Agility}.");
    }

    public override int Power => 8 * Level + 2 * Agility;

    private int StatLimit(int inputStat) => Validator.Limiter(inputStat, 0, 10);
}
