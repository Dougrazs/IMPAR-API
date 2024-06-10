using IMPAR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IMPAR_API.Data
{ 
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().ToTable("Car", "dbo");
        modelBuilder.Entity<Photo>().ToTable("Photo", "dbo");

        modelBuilder.Entity<Car>()
            .HasOne(c => c.Photo)
            .WithMany()
            .HasForeignKey(c => c.PhotoId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Car>? Cars { get; set; }
    public DbSet<Photo>? Photos { get; set; }
}
}