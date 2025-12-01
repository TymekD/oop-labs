namespace Simulator.Creatures;

public class Orc : Creature
{
    private int _rage;
    private int _huntCount;

    public int Rage
    {
        get => _rage;
        init => _rage = StatLimit(value);
    }

    public Orc()
    {
        _rage = StatLimit(1);
    }

    public Orc(string name, int level = 1, int agility = 1) : base(name, level)
    {
        _rage = StatLimit(agility);
    }

    // USUNIĘTE wyświetlanie komunikatów
    public void Hunt()
    {
        _huntCount++;
        if (_huntCount % 2 == 0)
        {
            _rage = StatLimit(_rage + 1);
        }
        // brak Console.WriteLine – metoda tylko zmienia stan
    }

    // ZAMIANA: void SayHi() -> string Greeting()
    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}, rage {Rage}.";
    }

    public override int Power => 7 * Level + 3 * Rage;
    public override string Info => $"{Name} [{Level}][{Rage}]";

    private int StatLimit(int inputStat) => Validator.Limiter(inputStat, 0, 10);
}
