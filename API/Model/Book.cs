namespace DefaultNamespace;

public class Book : Asset 
{
    public string Author { get; set; }
    public override string GetUsagePolicy() => "Library use: 7 days.";
}