using hexagonal.Data.Mappings;
using hexagonal.Domain;
using Microsoft.EntityFrameworkCore;

namespace hexagonal.Data.DataAccess;

public class HexagonalContext : DbContext
{
    public HexagonalContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SystemUser> SystemUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
    }    
}