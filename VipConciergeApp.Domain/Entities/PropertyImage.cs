namespace VipConciergeApp.Domain.Entities;

public class PropertyImage
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public Guid PropertyId { get; set; }

    public Property Property { get; set; } = null!;
}