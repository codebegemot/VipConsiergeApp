namespace VipConciergeApp.Domain.DTOs;

public class PropertyListItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string? MainImageUrl { get; set; }
}