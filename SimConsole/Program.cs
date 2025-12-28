using System.Text;
using Simulator;
using Simulator.Maps;
using Simulator.Creatures;
using SimConsole;

// Konfiguracja konsoli pod Unicode (ramki)
Console.OutputEncoding = Encoding.UTF8;

Console.Clear();
Console.WriteLine("Wybierz animację do uruchomienia:");
Console.WriteLine("1) Sim1 (przykład z instrukcji)");
Console.WriteLine("2) Sim2 (zwierzęta na mapie)");
Console.Write("Twój wybór (1/2): ");

char choice = ReadChoice('1', '2');
Console.Clear();

if (choice == '1')
{
    Run(Sim1());
}
else
{
    Run(Sim2());
}

Console.WriteLine();
Console.WriteLine("Symulacja zakończona. Naciśnij dowolny klawisz, aby zamknąć...");
Console.ReadKey(true);

static char ReadChoice(char a, char b)
{
    while (true)
    {
        var key = Console.ReadKey(true);
        if (key.KeyChar == a || key.KeyChar == b)
        {
            Console.WriteLine(key.KeyChar);
            return key.KeyChar;
        }
    }
}

static Simulation Sim1()
{
    SmallSquareMap map = new(5);

    List<IMappable> objects = new()
    {
        new Orc("Gorbag"),
        new Elf("Elandor")
    };

    List<Point> positions = new()
    {
        new Point(2, 2),
        new Point(3, 1)
    };

    string moves = "dlruldl";

    return new Simulation(map, objects, positions, moves);
}

static Simulation Sim2()
{
    // mapa torusowa 8 x 6
    SmallTorusMap map = new(8, 6);

    // elf, ork, stado królików, grupa orłów, grupa strusi
    List<IMappable> objects = new()
    {
        new Elf("Elandor"),
        new Orc("Gorbag"),
        new Animals("Rabbits") { Size = 12 },
        new Birds { Description = "Eagles", Size = 5, CanFly = true },
        new Birds { Description = "Ostriches", Size = 4, CanFly = false }
    };

    // Startowe pozycje (muszą być w granicach 8x6: x=0..7, y=0..5)
    List<Point> positions = new()
    {
        new Point(1, 1),
        new Point(2, 2),
        new Point(4, 1),
        new Point(6, 4),
        new Point(5, 3)
    };

    // 20 ruchów (U/R/D/L)
    string moves = "urdlurdlurdlurdlurdl"; // 20 znaków

    return new Simulation(map, objects, positions, moves);
}

static void Run(Simulation simulation)
{
    MapVisualizer visualizer = new(simulation.Map);

    Console.Clear();
    visualizer.Draw();
    Console.WriteLine("Naciśnij dowolny klawisz, aby wykonywać kolejne ruchy (Esc – wyjście).");

    while (!simulation.Finished)
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape)
            break;

        simulation.Turn();

        Console.Clear();
        visualizer.Draw();

        Console.WriteLine();
        Console.WriteLine($"Ruch: {simulation.CurrentMoveName}, obiekt: {simulation.CurrentCreature.Name}");
    }
}
