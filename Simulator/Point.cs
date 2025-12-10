namespace Simulator;

public readonly struct Point
{
    public readonly int X, Y;

    public Point(int x, int y) => (X, Y) = (x, y);

    public override string ToString() => $"({X}, {Y})";

    // Ruch o jedno pole w podanym kierunku
    public Point Next(Direction direction) =>
        direction switch
        {
            Direction.Up => new Point(X, Y + 1),
            Direction.Down => new Point(X, Y - 1),
            Direction.Left => new Point(X - 1, Y),
            Direction.Right => new Point(X + 1, Y),
            _ => this
        };

    // Ruch po skosie – kierunek obrócony o 45° zgodnie z ruchem wskazówek zegara
    // zgodnie z testami:
    // Up    -> (+1, +1)
    // Down  -> (-1, -1)
    // Left  -> (-1, +1)
    // Right -> (+1, -1)
    public Point NextDiagonal(Direction direction) =>
        direction switch
        {
            Direction.Up => new Point(X + 1, Y + 1),
            Direction.Down => new Point(X - 1, Y - 1),
            Direction.Left => new Point(X - 1, Y + 1),
            Direction.Right => new Point(X + 1, Y - 1),
            _ => this
        };
}
