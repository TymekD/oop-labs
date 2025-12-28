namespace Simulator.Creatures;

using Simulator;
using Simulator.Maps;

public class Birds : Animals
{
    public bool CanFly { get; set; } = true;

    // Na wizualizacji: ptaki latające B, nieloty b
    public override char Symbol => CanFly ? 'B' : 'b';

    public override string Info
    {
        get
        {
            var flyMark = CanFly ? "fly+" : "fly-";
            return $"{Description} ({flyMark}) <{Size}>";
        }
    }

    /// <summary>
    /// Birds movement:
    /// - flying: move 2 steps in given direction (Next twice)
    /// - flightless: move 1 step diagonally (NextDiagonal)
    /// </summary>
    public override void Go(Direction direction)
    {
        if (CurrentMap is null || Position is null)
            return;

        Point from = Position.Value;
        Point to;

        if (CanFly)
        {
            Point step1 = CurrentMap.Next(from, direction);
            Point step2 = CurrentMap.Next(step1, direction);
            to = step2;
        }
        else
        {
            to = CurrentMap.NextDiagonal(from, direction);
        }

        CurrentMap.Move(this, from, to);
    }
}
