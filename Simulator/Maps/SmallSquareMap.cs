using System;

namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public int Size { get; }

    // Prostokąt opisujący granice mapy: (0,0) do (Size-1, Size-1)
    private readonly Rectangle _bounds;

    public SmallSquareMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(
                nameof(size),
                "Size of SmallSquareMap must be between 5 and 20."
            );
        }

        Size = size;
        _bounds = new Rectangle(0, 0, Size - 1, Size - 1);
    }

    public override bool Exist(Point p)
    {
        // wykorzystujemy Rectangle do sprawdzania, czy punkt jest na mapie
        return _bounds.Contains(p);
    }

    public override Point Next(Point p, Direction d)
    {
        // NIE sprawdzamy, czy p jest poprawny – zgodnie z treścią zadania
        var candidate = p.Next(d);

        // jeśli wyjście poza mapę -> zostajemy w miejscu
        return Exist(candidate) ? candidate : p;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        var candidate = p.NextDiagonal(d);

        return Exist(candidate) ? candidate : p;
    }
}
