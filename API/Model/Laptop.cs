namespace DefaultNamespace;

public class Laptop : Asset 
{
    public string SerialNumber { get; set; }
    
    public string OS { get; set; }
    public override string GetUsagePolicy() => "Lab use: 24 hours.";
}
