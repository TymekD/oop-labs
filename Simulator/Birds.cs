namespace Simulator.Creatures;

public class Birds : Animals
{
    public bool CanFly { get; set; } = true;

    public override string Info
    {
        get
        {
            var flyMark = CanFly ? "fly+" : "fly-";
            return $"{Description} ({flyMark}) <{Size}>";
        }
    }
}