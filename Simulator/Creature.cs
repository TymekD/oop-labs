namespace Simulator;

public class Creature
{
    // Automatyczne właściwości
    public string Name { get; set; }
    public int Level { get; set; }

    // Konstruktor przyjmujący Name i opcjonalny Level
    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    // Konstruktor bezparametrowy, nic nie robiący
    public Creature()
    {
    }

    // Metoda SayHi()
    public void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name} at level {Level}!");
    }

    // Właściwość tylko do odczytu Info
    public string Info
    {
        get { return $"{Name} (level {Level})"; }
    }
}
