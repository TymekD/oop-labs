namespace Simulator;

internal class Validator
{
    public static int Limiter(int value, int min, int max) => Math.Clamp(value, min, max);

    public static string Shortener(string? value, int min, int max, char placeholder)
    {
        var formatted = (value ?? string.Empty).Trim();

        if (formatted.Length > max)
        {
            formatted = formatted[..max].TrimEnd();
        }

        if (formatted.Length < min)
        {
            formatted = formatted.PadRight(min, placeholder);
        }

        if (formatted.Length > 0 && char.IsLetter(formatted[0]) && char.IsLower(formatted[0]))
        {
            formatted = char.ToUpperInvariant(formatted[0]) + formatted[1..];
        }

        return formatted;
    }
}

