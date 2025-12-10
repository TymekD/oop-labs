using Simulator;

namespace TestSimulator;

public class ValidatorTests
{
    [Theory]
    [InlineData(5, 0, 10, 5)]
    [InlineData(-5, 0, 10, 0)]
    [InlineData(15, 0, 10, 10)]
    [InlineData(0, 0, 10, 0)]
    [InlineData(10, 0, 10, 10)]
    public void Limiter_ShouldClampValue(int value, int min, int max, int expected)
    {
        var result = Validator.Limiter(value, min, max);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Shortener_NullOrEmpty_ShouldPadWithPlaceholderToMin()
    {
        var resultNull = Validator.Shortener(null, 3, 10, '#');
        var resultEmpty = Validator.Shortener("   ", 3, 10, '#');

        Assert.Equal("###", resultNull);
        Assert.Equal("###", resultEmpty);
    }

    [Fact]
    public void Shortener_ShortString_ShouldPadAndCapitalizeFirstLetter()
    {
        var result = Validator.Shortener("ab", 3, 10, '#');

        // "ab" -> "ab#" -> "Ab#"
        Assert.Equal("Ab#", result);
    }

    [Fact]
    public void Shortener_LongString_ShouldBeTrimmedToMax_AndFirstLetterCapitalized()
    {
        var result = Validator.Shortener("   abcdefghijkl  ", 3, 5, '#');

        // "abcdefghijkl" -> "abcde" -> "Abcde"
        Assert.Equal("Abcde", result);
    }

    [Fact]
    public void Shortener_FirstCharNonLetter_ShouldNotChangeCase()
    {
        var result = Validator.Shortener("   1abcde   ", 3, 10, '#');

        // "1abcde" – pierwszy znak nie jest literą, więc bez zmiany wielkości
        Assert.Equal("1abcde", result);
    }

    [Fact]
    public void Shortener_FirstLetterAlreadyUppercase_ShouldStayUppercase()
    {
        var result = Validator.Shortener("   Zorro  ", 3, 10, '#');

        Assert.Equal("Zorro", result);
    }
}
