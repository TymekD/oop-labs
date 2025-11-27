namespace Simulator;

public class Animals
{
    private string _description = "Unknown";

    public string Description
    {
        get => _description;
        init => _description = Validator.Shortener(value, 3, 15, '#');
    }
    public uint Size { get; set; } = 3;

    public Animals()
    {

    }

    public Animals(string description)
    {
        Description = description;
    }

    public virtual string Info => $"{Description} <{Size}>";

    public override string ToString()
    {
        var typeName = GetType().Name.ToUpperInvariant();
        return $"{typeName}: {Info}";
    }    
    /*
 private string ValidationDesc(string inputDesc)
 {
     string newDesc = (inputDesc ?? "").Trim();

     if (newDesc.Length > 15)
     {
         newDesc = newDesc.Substring(0, 15).TrimEnd();
     }

     if (newDesc.Length < 3)
     {
         newDesc = newDesc.PadRight(3, '#');
     }

     if (newDesc.Length > 0 && char.IsLower(newDesc[0]))
     {
         newDesc = char.ToUpper(newDesc[0]) + newDesc.Substring(1);
     }

     return newDesc;
 }
 */
}

