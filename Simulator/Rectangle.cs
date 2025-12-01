using System;

namespace Simulator;
public class Rectangle
{
    public readonly int X1;
    public readonly int Y1;
    public readonly int X2;
    public readonly int Y2;

    public Rectangle(int x1, int y1, int x2, int y2)
    {
        int minX = Math.Min(x1, x2);
        int maxX = Math.Max(x1, x2);
        int minY = Math.Min(y1, y2);
        int maxY = Math.Max(y1, y2);

        if (minX == maxX || minY == maxY)
        {
            throw new ArgumentException("Punkty nie mogą być współliniowe – prostokąt musi mieć dodatnią szerokość i wysokość.");
        }

        X1 = minX;
        Y1 = minY;
        X2 = maxX;
        Y2 = maxY;
    }

    public Rectangle(Point p1, Point p2)
        : this(p1.X, p1.Y, p2.X, p2.Y)
    {
    }

    /// <summary>
    /// Sprawdza, czy prostokąt zawiera punkt (łącznie z krawędziami).
    /// </summary>
    public bool Contains(Point point)
    {
        return point.X >= X1 && point.X <= X2
            && point.Y >= Y1 && point.Y <= Y2;
    }

    public override string ToString() => $"({X1}, {Y1}):({X2}, {Y2})";
}
