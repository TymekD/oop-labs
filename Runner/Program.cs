using Simulator;
using Simulator.Creatures;

namespace Runner;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");

        //TestValidation();
        //Console.WriteLine("\n");
        //TestObjectsToString();
        //Console.WriteLine("\n");
        //TestElfsAndOrcs();
        //Console.WriteLine("\n");
        //TestDirections();
        Point p = new(10, 25);
        Console.WriteLine(p.Next(Direction.Right));          // (11, 25)
        Console.WriteLine(p.NextDiagonal(Direction.Right));  // (11, 24)
    }

    static void TestElfsAndOrcs()
    {
        Console.WriteLine("HUNT TEST\n");
        var o = new Orc() { Name = "Gorbag", Rage = 7 };
        Console.WriteLine(o.Greeting());
        for (int i = 0; i < 10; i++)
        {
            o.Hunt();
            Console.WriteLine(o.Greeting());
        }

        Console.WriteLine("\nSING TEST\n");
        var e = new Elf("Legolas", agility: 2);
        Console.WriteLine(e.Greeting());
        for (int i = 0; i < 10; i++)
        {
            e.Sing();
            Console.WriteLine(e.Greeting());
        }

        Console.WriteLine("\nPOWER TEST\n");
        Creature[] creatures =
        {
            o,
            e,
            new Orc("Morgash", 3, 8),
            new Elf("Elandor", 5, 3)
        };

        foreach (Creature creature in creatures)
        {
            Console.WriteLine($"{creature.Name,-15}: {creature.Power}");
        }
    }

    static void TestValidation()
    {
        Creature c = new Orc() { Name = "   Shrek    ", Level = 20 };
        Console.WriteLine(c.Greeting());
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Orc("  ", -5);
        Console.WriteLine(c.Greeting());
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Orc("  donkey ") { Level = 7 };
        Console.WriteLine(c.Greeting());
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Orc("Puss in Boots – a clever and brave cat.");
        Console.WriteLine(c.Greeting());
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Orc("a                            troll name", 5);
        Console.WriteLine(c.Greeting());
        c.Upgrade();
        Console.WriteLine(c.Info);

        var a = new Animals() { Description = "   Cats " };
        Console.WriteLine(a.Info);

        a = new Animals() { Description = "Mice           are great", Size = 40 };
        Console.WriteLine(a.Info);
    }

    private static void TestObjectsToString()
    {
        object[] myObjects =
        {
            new Animals() { Description = "dogs"},
            new Birds { Description = "  eagles ", Size = 10 },
            new Elf("e", 15, -3),
            new Orc("morgash", 6, 4),
        };
        Console.WriteLine("\nMy objects:");
        foreach (var o in myObjects)
        {
            Console.WriteLine(o);
        }
    }
}
