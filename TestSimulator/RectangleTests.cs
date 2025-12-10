using Simulator;
using Xunit;

namespace TestSimulator;

public class RectangleTests
{
    [Fact]
    public void Constructor_ProperOrder_ShouldKeepCoordinates()
    {
        var r = new Rectangle(1, 2, 5, 7);

        Assert.Equal(1, r.X1);
        Assert.Equal(2, r.Y1);
        Assert.Equal(5, r.X2);
        Assert.Equal(7, r.Y2);
    }

    [Fact]
    public void Constructor_ReverseOrder_ShouldSwapCoordinates()
    {
        var r = new Rectangle(5, 7, 1, 2);

        Assert.Equal(1, r.X1);
        Assert.Equal(2, r.Y1);
        Assert.Equal(5, r.X2);
        Assert.Equal(7, r.Y2);
    }

    [Theory]
    [InlineData(0, 0, 5, 0)]  // linia pozioma
    [InlineData(0, 0, 0, 5)]  // linia pionowa
    public void Constructor_ColinearPoints_ShouldThrowArgumentException(
        int x1, int y1, int x2, int y2)
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(x1, y1, x2, y2));
    }

    [Fact]
    public void Contains_ShouldReturnTrue_ForPointsInsideAndOnBorder()
    {
        var r = new Rectangle(1, 2, 5, 7);

        Assert.True(r.Contains(new Point(1, 2))); // lewy dół
        Assert.True(r.Contains(new Point(5, 7))); // prawy góra
        Assert.True(r.Contains(new Point(3, 4))); // środek
    }

    [Fact]
    public void Contains_ShouldReturnFalse_ForPointsOutside()
    {
        var r = new Rectangle(1, 2, 5, 7);

        Assert.False(r.Contains(new Point(0, 2)));
        Assert.False(r.Contains(new Point(1, 1)));
        Assert.False(r.Contains(new Point(6, 7)));
        Assert.False(r.Contains(new Point(5, 8)));
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        var r = new Rectangle(1, 2, 5, 7);
        Assert.Equal("(1, 2):(5, 7)", r.ToString());
    }
}
