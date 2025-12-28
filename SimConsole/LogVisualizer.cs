using System;
using Simulator;

namespace SimConsole;

internal class LogVisualizer
{
    public SimulationLog Log { get; }

    public LogVisualizer(SimulationLog log)
    {
        Log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public void Draw(int turnIndex)
    {
        if (turnIndex < 0 || turnIndex >= Log.TurnLogs.Count)
            throw new ArgumentOutOfRangeException(nameof(turnIndex));

        int width = Log.SizeX;
        int height = Log.SizeY;

        var symbols = Log.TurnLogs[turnIndex].Symbols;

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

        // WNĘTRZE
        for (int y = height - 1; y >= 0; y--)
        {
            Console.Write(Box.Vertical);

            for (int x = 0; x < width; x++)
            {
                var p = new Point(x, y);
                char c = symbols.TryGetValue(p, out var s) ? s : ' ';

                Console.Write(c);

                if (x < width - 1)
                    Console.Write(Box.Vertical);
            }

            Console.Write(Box.Vertical);
            Console.WriteLine();

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
