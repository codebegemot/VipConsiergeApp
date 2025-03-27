using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VipConciergeApp.Domain.DTOs;
using VipConciergeApp.Domain.Entities;
using VipConciergeApp.Infrastructure.Data;

namespace VipConciergeApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext  _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        var properties = await _context.Properties
            .Include(p => p.Images)
            .Select(p => new PropertyListItemDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                MainImageUrl = p.Images.FirstOrDefault()!.ImageUrl
            })
            .ToListAsync();

        return Ok(properties);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProperty(Guid id)
    {
        var property = await _context.Properties
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property == null)
            return NotFound();

        var dto = new PropertyDetailsDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Price = property.Price,
            CreatedAt = property.CreatedAt,
            Images = property.Images.Select(img => new PropertyImageDto
            {
                Id = img.Id,
                ImageUrl = img.ImageUrl
            }).ToList()
        };

        return Ok(dto);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateProperty(CreatePropertyDto dto)
    {
        var property = new Property
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProperty(Guid id, CreatePropertyDto dto)
    {
        var property = await _context.Properties.FindAsync(id);
        
        if (property == null)
            return NotFound();
        
        property.Title = dto.Title;
        property.Description = dto.Description;
        property.Price = dto.Price;

        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);

        if (property == null)
            return NotFound();
        
        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}