namespace API.Model;

public class Laptop(string name, string? serialNumber, string? os) : Asset(name)
{
    public Laptop() : this(string.Empty, string.Empty, string.Empty) { }
    public string SerialNumber { get; set; } = string.IsNullOrWhiteSpace(serialNumber) ? "SN-PENDING" : serialNumber;
    
    public string OS { get; set; } = string.IsNullOrWhiteSpace(os) ? "Windows 11" : os;
}
