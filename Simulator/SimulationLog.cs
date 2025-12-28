using System;
using System.Collections.Generic;
using Simulator.Maps;

namespace Simulator;

/// <summary>
/// Executes the whole simulation once and stores minimal data required
/// to reproduce any turn without re-running moves.
/// </summary>
public class SimulationLog
{
    private Simulation _simulation { get; }

    public int SizeX { get; }
    public int SizeY { get; }

    /// <summary>
    /// Logs for turns:
    /// index 0 = starting positions (before any moves)
    /// index i = state after i-th move (i >= 1)
    /// </summary>
    public List<TurnLog> TurnLogs { get; } = [];

    public SimulationLog(Simulation simulation)
    {
        _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        // index 0: initial state (no mover yet)
        TurnLogs.Add(new TurnLog
        {
            Mappable = "START",
            Move = "",
            Symbols = CaptureSymbols(_simulation.Map)
        });

        // next indices: after each simulation turn
        while (!_simulation.Finished)
        {
            string mappableText = _simulation.CurrentCreature.ToString();
            string moveText = _simulation.CurrentMoveName;

            _simulation.Turn();

            TurnLogs.Add(new TurnLog
            {
                Mappable = mappableText,
                Move = moveText,
                Symbols = CaptureSymbols(_simulation.Map)
            });
        }
    }

    private static Dictionary<Point, char> CaptureSymbols(Map map)
    {
        var dict = new Dictionary<Point, char>();

        for (int y = 0; y < map.SizeY; y++)
        {
            for (int x = 0; x < map.SizeX; x++)
            {
                var mappables = map.At(x, y);

                if (mappables.Count == 0)
                    continue;

                char symbol = (mappables.Count > 1)
                    ? 'X'
                    : mappables[0].Symbol;

                dict[new Point(x, y)] = symbol;
            }
        }

        return dict;
    }
}
