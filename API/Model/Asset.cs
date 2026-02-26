namespace API.Model;

public abstract class Asset(string name)
{
    private Condition _condition;

    public int Id { get; set; }
    public string Name { get; set; } = name;

    public Condition ItemCondition
    {
        get => _condition;
        set
        {
            //Don't allow the condition to improve.
            if (value >= _condition)
            {
                _condition = value;
            }
        }
    }

    public int? CurrentMemberId { get; set; }
}