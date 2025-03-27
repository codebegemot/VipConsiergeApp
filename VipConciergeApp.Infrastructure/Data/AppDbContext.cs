using Microsoft.EntityFrameworkCore;
using VipConciergeApp.Domain.Entities;

namespace VipConciergeApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public  AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}
    
    public DbSet<Property>  Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
        });
        
        modelBuilder.Entity<PropertyImage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ImageUrl).IsRequired();
        });
    }
}