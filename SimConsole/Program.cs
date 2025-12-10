using System.Text;
using Simulator;
using Simulator.Maps;
using SimConsole;
using Simulator.Creatures;

// Konfiguracja konsoli pod Unicode (ramki)
Console.OutputEncoding = Encoding.UTF8;

// Przykładowe dane z instrukcji:
SmallSquareMap map = new(5);
List<Creature> creatures = new() { new Orc("Gorbag"), new Elf("Elandor") };
List<Point> points = new() { new Point(2, 2), new Point(3, 1) };
string moves = "dlruldl";

Simulation simulation = new(map, creatures, points, moves);
MapVisualizer visualizer = new(simulation.Map);

// Pierwszy rysunek
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
    Console.WriteLine($"Ruch: {simulation.CurrentMoveName}, " +
                      $"stworzenie: {simulation.CurrentCreature.Name}");
}

Console.WriteLine();
Console.WriteLine("Symulacja zakończona. Naciśnij dowolny klawisz, aby zamknąć...");
Console.ReadKey(true);
