namespace VipConciergeApp.Domain.DTOs;

public class CreatePropertyDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}