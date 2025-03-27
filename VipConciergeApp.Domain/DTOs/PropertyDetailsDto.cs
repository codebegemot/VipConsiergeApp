namespace VipConciergeApp.Domain.DTOs;

public class PropertyDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PropertyImageDto> Images { get; set; } = new();
}