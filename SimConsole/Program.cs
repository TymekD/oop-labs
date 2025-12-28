using System.Text;
using Simulator;
using Simulator.Maps;
using Simulator.Creatures;
using SimConsole;

// Konfiguracja konsoli pod Unicode (ramki)
Console.OutputEncoding = Encoding.UTF8;

Console.Clear();
Console.WriteLine("Wybierz animację:");
Console.WriteLine("1) Sim1 (stary przykład)");
Console.WriteLine("2) Sim2 (zwierzęta na mapie)");
Console.WriteLine("3) Sim3 (historia: pokaż tury 5/10/15/20 z Sim2)");
Console.Write("Twój wybór (1/2/3): ");

char choice = ReadChoice('1', '2', '3');
Console.Clear();

switch (choice)
{
    case '1':
        RunAnimated(Sim1());
        break;

    case '2':
        RunAnimated(Sim2());
        break;

    case '3':
        RunHistory(Sim3());
        break;
}

Console.WriteLine();
Console.WriteLine("Koniec. Naciśnij dowolny klawisz...");
Console.ReadKey(true);

static char ReadChoice(params char[] allowed)
{
    while (true)
    {
        var key = Console.ReadKey(true).KeyChar;
        foreach (var c in allowed)
        {
            if (key == c)
            {
                Console.WriteLine(key);
                return key;
            }
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

    List<Point> positions = new()
    {
        new Point(1, 1),
        new Point(2, 2),
        new Point(4, 1),
        new Point(6, 4),
        new Point(5, 3)
    };

    // 20 ruchów
    string moves = "urdlurdlurdlurdlurdl";

    return new Simulation(map, objects, positions, moves);
}

/// <summary>
/// Sim3 uses the same setup as Sim2, but returns a SimulationLog (history).
/// </summary>
static SimulationLog Sim3()
{
    // WAŻNE: SimulationLog wykonuje symulację w konstruktorze i ją “zużywa”.
    // Dlatego tworzymy nową symulację (taką samą jak w Sim2) i logujemy ją.
    var sim = Sim2();
    return new SimulationLog(sim);
}

static void RunAnimated(Simulation simulation)
{
    MapVisualizer visualizer = new(simulation.Map);

    Console.Clear();
    visualizer.Draw();
    Console.WriteLine("Dowolny klawisz = następny ruch | Esc = wyjście");

    while (!simulation.Finished)
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape)
            break;

        simulation.Turn();

        Console.Clear();
        visualizer.Draw();

        Console.WriteLine();
        Console.WriteLine($"Następny ruch: {simulation.CurrentMoveName}");
        Console.WriteLine($"Następny obiekt: {simulation.CurrentCreature.Name}");
    }
}

static void RunHistory(SimulationLog log)
{
    LogVisualizer visualizer = new(log);

    int[] turnsToShow = { 5, 10, 15, 20 };

    foreach (int t in turnsToShow)
    {
        Console.Clear();

        // turn index in log: 0 is START, so "5th turn" = index 5
        if (t < 0 || t >= log.TurnLogs.Count)
        {
            Console.WriteLine($"Nie mogę wyświetlić tury {t} (log ma {log.TurnLogs.Count - 1} tur).");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey(true);
            continue;
        }

        var turn = log.TurnLogs[t];

        Console.WriteLine($"TURA: {t}");
        Console.WriteLine($"Ruch wykonał: {turn.Mappable}");
        Console.WriteLine($"Ruch: {turn.Move}");
        Console.WriteLine();

        visualizer.Draw(t);

        Console.WriteLine();
        Console.WriteLine("Naciśnij dowolny klawisz, aby przejść dalej...");
        Console.ReadKey(true);
    }
}