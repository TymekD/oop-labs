using System;
using Simulator;
using Simulator.Maps;

namespace SimConsole;

public class MapVisualizer
{
    private readonly Map _map;

    public MapVisualizer(Map map)
    {
        _map = map ?? throw new ArgumentNullException(nameof(map));
    }

    private char GetCellSymbol(int x, int y)
    {
        var creatures = _map.At(x, y);

        if (creatures.Count == 0)
            return ' ';

        if (creatures.Count > 1)
            return 'X';

        return creatures[0].Symbol;
    }

    public void Draw()
    {
        int width = _map.SizeX;
        int height = _map.SizeY;

        // GÓRNA RAMKA
        Console.Write(Box.TopLeft);
        for (int x = 0; x < width; x++)
        {
            Console.Write(Box.Horizontal);
            if (x < width - 1)
                Console.Write(Box.TopMid);
        }
        Console.Write(Box.TopRight);
        Console.WriteLine();

        // Wiersze pól – rysujemy od "góry", żeby Direction.Up faktycznie szedł w górę
        for (int y = height - 1; y >= 0; y--)
        {
            // Lewa krawędź
            Console.Write(Box.Vertical);

            for (int x = 0; x < width; x++)
            {
                Console.Write(GetCellSymbol(x, y));
                Console.Write(Box.Vertical);
            }

            Console.WriteLine();

            // Linie poziome między wierszami (poza ostatnim)
            if (y > 0)
            {
                Console.Write(Box.MidLeft);
                for (int x = 0; x < width; x++)
                {
                    Console.Write(Box.Horizontal);
                    if (x < width - 1)
                        Console.Write(Box.Cross);
                }
                Console.Write(Box.MidRight);
                Console.WriteLine();
            }
        }

        // DOLNA RAMKA
        Console.Write(Box.BottomLeft);
        for (int x = 0; x < width; x++)
        {
            Console.Write(Box.Horizontal);
            if (x < width - 1)
                Console.Write(Box.BottomMid);
        }
        Console.Write(Box.BottomRight);
        Console.WriteLine();
    }
}
