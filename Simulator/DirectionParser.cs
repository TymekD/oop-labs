namespace Simulator;

public static class DirectionParser
{
    public static Direction[] Parse(string directionsString)
    {
        if (string.IsNullOrEmpty(directionsString))
        {
            return new Direction[0]; 
        }

        List<Direction> parsedDirections = new List<Direction>();

        foreach (char c in directionsString)
        {
            char upperChar = char.ToUpper(c);

            switch (upperChar)
            {
                case 'U':
                    parsedDirections.Add(Direction.Up);
                    break;
                case 'R':
                    parsedDirections.Add(Direction.Right);
                    break;
                case 'D':
                    parsedDirections.Add(Direction.Down);
                    break;
                case 'L':
                    parsedDirections.Add(Direction.Left);
                    break;
            }
        }

        return parsedDirections.ToArray();
    }
}