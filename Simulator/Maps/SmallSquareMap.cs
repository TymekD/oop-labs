namespace Simulator.Maps;

internal class SmallSquareMap : Map
{
    public int Size { get; }

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
        return _bounds.Contains(p);
    }

    public override Point Next(Point p, Direction d)
    {
        Point candidate = p.Next(d);

        return Exist(candidate) ? candidate : p;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point candidate = p.NextDiagonal(d);

        return Exist(candidate) ? candidate : p;
    }
}
