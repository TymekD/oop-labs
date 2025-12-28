using System;
using System.Collections.Generic;
using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<IMappable> Creatures { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves (raw string).
    /// Bad moves are ignored - use DirectionParser.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// Parsed and cleaned list of directions (invalid chars removed).
    /// </summary>
    private readonly Direction[] _directions;

    /// <summary>
    /// Index of current move (0-based).
    /// </summary>
    private int _currentMoveIndex = 0;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public IMappable CurrentCreature
        => Creatures[_currentMoveIndex % Creatures.Count];

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// Returns empty string when there are no moves or simulation finished.
    /// </summary>
    public string CurrentMoveName
    {
        get
        {
            if (Finished || _directions.Length == 0)
            {
                return string.Empty;
            }

            var dir = _directions[_currentMoveIndex];
            return dir.ToString().ToLowerInvariant();
        }
    }

    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<IMappable> creatures,
        List<Point> positions, string moves)
    {
        Map = map ?? throw new ArgumentNullException(nameof(map));
        Creatures = creatures ?? throw new ArgumentNullException(nameof(creatures));
        Positions = positions ?? throw new ArgumentNullException(nameof(positions));
        Moves = moves ?? string.Empty;

        if (Creatures.Count == 0)
        {
            throw new ArgumentException("Creatures list cannot be empty.",
                nameof(creatures));
        }

        if (Creatures.Count != Positions.Count)
        {
            throw new ArgumentException(
                "Number of creatures must be equal to number of starting positions.",
                nameof(positions));
        }

        // Parse moves – invalid characters are ignored by DirectionParser.
        _directions = DirectionParser.Parse(Moves);

        // If there are no valid moves at all, simulation is finished from the start.
        if (_directions.Length == 0)
        {
            Finished = true;
        }

        // Put all creatures on the map on their starting positions.
        for (int i = 0; i < Creatures.Count; i++)
        {
            Map.Add(Creatures[i], Positions[i]);
        }
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        if (Finished)
        {
            throw new InvalidOperationException("Simulation is already finished.");
        }

        if (_directions.Length == 0)
        {
            throw new InvalidOperationException("Simulation has no valid moves.");
        }

        var direction = _directions[_currentMoveIndex];
        var creature = CurrentCreature;

        // Creature uses its current map's rules to move.
        creature.Go(direction);

        _currentMoveIndex++;

        if (_currentMoveIndex >= _directions.Length)
        {
            Finished = true;
        }
    }
}
