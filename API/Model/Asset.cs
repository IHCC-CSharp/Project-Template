namespace DefaultNamespace;

public abstract class Asset 
{
    private Condition _condition; // Backing field for logic

    public int Id { get; set; }
    public string Name { get; set; }

    // LU02/LU06: Property with Logic
    public Condition ItemCondition 
    { 
        get => _condition; 
        set 
        {
            // If the new value is "worse" (higher int) or the same, allow it.
            // This prevents a 'Broken' item from being set back to 'New'.
            if (value >= _condition) 
            {
                _condition = value;
            }
        } 
    }

    // LU04/05: The Relationship
    // Link to the Member table
    public int? CurrentMemberId { get; set; } 
    
    // Navigation property for C# logic
    public Member? CurrentHolder { get; set; }

    public abstract string GetUsagePolicy();
}