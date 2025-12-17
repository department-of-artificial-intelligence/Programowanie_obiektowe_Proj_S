namespace RatingSystem.Domain;

public class Service
{
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ServiceType { get; set; }
    public ICollection<Rating?> Ratings { get; set; }
    

    public Service( string name, string description, string? serviceType)
    {
        Name = name;
        Description = description;
        ServiceType = serviceType;

    }

    public Service() {  }
    
}