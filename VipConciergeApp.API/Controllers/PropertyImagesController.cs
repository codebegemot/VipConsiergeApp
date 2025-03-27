using Microsoft.AspNetCore.Mvc;
using VipConciergeApp.Domain.DTOs;
using VipConciergeApp.Domain.Entities;
using VipConciergeApp.Infrastructure.Data;

namespace VipConciergeApp.Controllers;
[ApiController]
[Route("api/properties/{propertyId:guid}/images")]
public class PropertyImagesController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly AppDbContext _context;

    public PropertyImagesController(IWebHostEnvironment env, AppDbContext context)
    {
        _env = env;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(Guid propertyId, IFormFile? file)
    {
        var property = await _context.Properties.FindAsync(propertyId);
        if (property == null)
            return NotFound("Property not found.");

        if (file == null || file.Length == 0)
            return BadRequest("No file selected.");

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var propertyImage = new PropertyImage
        {
            Id = Guid.NewGuid(),
            PropertyId = propertyId,
            ImageUrl = $"/images/{fileName}"
        };

        _context.Add(propertyImage);
        await _context.SaveChangesAsync();

        var dto = new PropertyImageDto
        {
            Id = propertyImage.Id,
            ImageUrl = propertyImage.ImageUrl
        };

        return Ok(dto);
    }

}