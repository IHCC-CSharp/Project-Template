namespace API.Model;

public class Book(string name, string? author) : Asset(name)
{
    public Book() : this(string.Empty, string.Empty) { }
    public string Author { get; set; } = string.IsNullOrWhiteSpace(author) ? "Unknown Author" : author;
}