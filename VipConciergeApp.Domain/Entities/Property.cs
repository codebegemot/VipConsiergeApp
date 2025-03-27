namespace VipConciergeApp.Domain.Entities;

public class Property
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
}