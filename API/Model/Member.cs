namespace API.Model;

public class Member(string name, string email) 
{
    public Member() : this(string.Empty, string.Empty) { }
    
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
}