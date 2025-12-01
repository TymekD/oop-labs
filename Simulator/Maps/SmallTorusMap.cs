using System;

namespace Simulator.Maps;

public class SmallTorusMap : Map
{
    public int Size { get; }

    public SmallTorusMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(
                nameof(size),
                "Size of SmallTorusMap must be between 5 and 20."
            );
        }

        Size = size;
    }

    public override bool Exist(Point p)
    {
        // Punkt należy do mapy, jeśli jest w zakresie [0, Size-1] po obu osiach
        return p.X >= 0 && p.X < Size
            && p.Y >= 0 && p.Y < Size;
    }

    private int Wrap(int coord)
    {
        // Proste zawijanie torusa dla jednej współrzędnej
        if (coord < 0) return Size - 1;
        if (coord >= Size) return 0;
        return coord;
    }

    public override Point Next(Point p, Direction d)
    {
        int dx = 0, dy = 0;

        switch (d)
        {
            case Direction.Up:
                dy = 1;
                break;
            case Direction.Down:
                dy = -1;
                break;
            case Direction.Left:
                dx = -1;
                break;
            case Direction.Right:
                dx = 1;
                break;
        }

        int newX = Wrap(p.X + dx);
        int newY = Wrap(p.Y + dy);

        return new Point(newX, newY);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        int dx = 0, dy = 0;

        // Kierunek obrócony o 45° zgodnie z ruchem wskazówek zegara:
        // Up    -> ( +1, +1 )
        // Down  -> ( -1, -1 )
        // Left  -> ( -1, +1 )
        // Right -> ( +1, -1 )
        switch (d)
        {
            case Direction.Up:
                dx = 1;
                dy = 1;
                break;
            case Direction.Down:
                dx = -1;
                dy = -1;
                break;
            case Direction.Left:
                dx = -1;
                dy = 1;
                break;
            case Direction.Right:
                dx = 1;
                dy = -1;
                break;
        }

        int newX = Wrap(p.X + dx);
        int newY = Wrap(p.Y + dy);

        return new Point(newX, newY);
    }
}
