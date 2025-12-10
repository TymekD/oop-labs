using Simulator;

namespace TestSimulator;

public class PointTests
{
    [Fact]
    public void Constructor_And_ToString_ShouldWorkCorrectly()
    {
        var p = new Point(10, 25);

        Assert.Equal(10, p.X);
        Assert.Equal(25, p.Y);
        Assert.Equal("(10, 25)", p.ToString());
    }

    [Fact]
    public void Next_ShouldMoveOneStepInGivenDirection()
    {
        var p = new Point(10, 25);

        Assert.Equal(new Point(10, 26), p.Next(Direction.Up));
        Assert.Equal(new Point(10, 24), p.Next(Direction.Down));
        Assert.Equal(new Point(9, 25), p.Next(Direction.Left));
        Assert.Equal(new Point(11, 25), p.Next(Direction.Right));
    }

    [Fact]
    public void NextDiagonal_ShouldMoveOneStepDiagonal_Rotated45Clockwise()
    {
        var p = new Point(10, 25);

        // przykład z zadania
        Assert.Equal(new Point(11, 24), p.NextDiagonal(Direction.Right));

        // pozostałe kierunki zgodnie z ustaloną logiką:
        Assert.Equal(new Point(11, 26), p.NextDiagonal(Direction.Up));    // (+1, +1)
        Assert.Equal(new Point(9, 24), p.NextDiagonal(Direction.Down));  // (-1, -1)
        Assert.Equal(new Point(9, 26), p.NextDiagonal(Direction.Left));  // (-1, +1)
    }
}
