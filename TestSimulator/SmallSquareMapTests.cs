using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallSquareMapTests
{
    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    public void Constructor_ValidSize_ShouldSetSize(int size)
    {
        // Act
        var map = new SmallSquareMap(size);

        // Assert
        Assert.Equal(size, map.Size);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(21)]
    public void Constructor_InvalidSize_ShouldThrowArgumentOutOfRangeException(int size)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(size));
    }

    [Fact]
    public void Exist_BorderAndInsidePoints_ShouldReturnTrue()
    {
        // Arrange
        var map = new SmallSquareMap(10);

        // Act & Assert
        Assert.True(map.Exist(new Point(0, 0)));          // lewy dolny róg
        Assert.True(map.Exist(new Point(9, 9)));          // prawy górny róg
        Assert.True(map.Exist(new Point(5, 3)));          // punkt wewnętrzny
    }

    [Fact]
    public void Exist_OutsidePoints_ShouldReturnFalse()
    {
        // Arrange
        var map = new SmallSquareMap(10);

        // Act & Assert
        Assert.False(map.Exist(new Point(-1, 0)));
        Assert.False(map.Exist(new Point(0, -1)));
        Assert.False(map.Exist(new Point(10, 0)));
        Assert.False(map.Exist(new Point(0, 10)));
        Assert.False(map.Exist(new Point(20, 20)));
    }

    [Theory]
    [InlineData(5, 5, Direction.Up, 5, 6)]   // zwykły ruch w górę
    [InlineData(5, 5, Direction.Down, 5, 4)]
    [InlineData(5, 5, Direction.Left, 4, 5)]
    [InlineData(5, 5, Direction.Right, 6, 5)]
    public void Next_InsideMap_ShouldMoveOneStep(int x, int y,
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallSquareMap(10);
        var p = new Point(x, y);

        // Act
        var result = map.Next(p, direction);

        // Assert
        Assert.Equal(new Point(expectedX, expectedY), result);
    }

    [Theory]
    [InlineData(0, 0, Direction.Down)]          // dół
    [InlineData(0, 0, Direction.Left)]          // lewo
    [InlineData(9, 9, Direction.Up)]            // góra
    [InlineData(9, 9, Direction.Right)]         // prawo
    public void Next_AtBorder_ShouldStayInPlace_WhenLeavingSquare(int x, int y, Direction direction)
    {
        // Arrange
        var map = new SmallSquareMap(10);
        var p = new Point(x, y);

        // Act
        var result = map.Next(p, direction);

        // Assert – próba wyjścia poza mapę → pozostajemy w miejscu
        Assert.Equal(p, result);
    }

    [Theory]
    [InlineData(5, 5, Direction.Up, 6, 6)]
    [InlineData(5, 5, Direction.Down, 4, 4)]
    [InlineData(5, 5, Direction.Left, 4, 6)]
    [InlineData(5, 5, Direction.Right, 6, 4)]
    public void NextDiagonal_InsideMap_ShouldMoveOneStepDiagonal(int x, int y,
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallSquareMap(10);
        var p = new Point(x, y);

        // Act
        var result = map.NextDiagonal(p, direction);

        // Assert
        Assert.Equal(new Point(expectedX, expectedY), result);
    }

    [Theory]
    [InlineData(0, 0, Direction.Down)]
    [InlineData(0, 0, Direction.Left)]
    [InlineData(9, 9, Direction.Up)]
    [InlineData(9, 9, Direction.Right)]
    public void NextDiagonal_AtBorder_ShouldStayInPlace_WhenLeavingSquare(int x, int y, Direction direction)
    {
        // Arrange
        var map = new SmallSquareMap(10);
        var p = new Point(x, y);

        // Act
        var result = map.NextDiagonal(p, direction);

        // Assert
        Assert.Equal(p, result);
    }
}
