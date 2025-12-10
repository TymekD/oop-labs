using System;

namespace Simulator.Maps;

public class SmallTorusMap : Map
{
    public SmallTorusMap(int SizeX, int SizeY)
        : base(SizeX, SizeY)
    {
        if (SizeX > 20)
        {
            throw new ArgumentOutOfRangeException(
                nameof(SizeX),
                "Size of SmallTorusMap must be between 5 and 20."
            );
        }
        if (SizeY > 20)
        {
            throw new ArgumentOutOfRangeException(
                nameof(SizeY),
                "Size of SmallTorusMap must be between 5 and 20."

            );
        }
    }

    /// <summary>
    /// Backwards-compatible constructor for square torus maps.
    /// </summary>
    public SmallTorusMap(int size)
        : this(size, size)
    {
    }

    private int WrapX(int x)
    {
        if (x < 0) return SizeX - 1;
        if (x >= SizeX) return 0;
        return x;
    }

    private int WrapY(int y)
    {
        if (y < 0) return SizeY - 1;
        if (y >= SizeY) return 0;
        return y;
    }

    public override Point Next(Point p, Direction d)
    {
        var candidate = p.Next(d);

        int newX = WrapX(candidate.X);
        int newY = WrapY(candidate.Y);

        return new Point(newX, newY);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        // Kierunek obrócony o 45° zgodnie z ruchem wskazówek zegara:
        // Up    -> ( +1, +1 )
        // Down  -> ( -1, -1 )
        // Left  -> ( -1, +1 )
        // Right -> ( +1, -1 )
        var candidate = p.NextDiagonal(d);

        int newX = WrapX(candidate.X);
        int newY = WrapY(candidate.Y);


        return new Point(newX, newY);
    }
}
