using hexagonal.Domain.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hexagonal.Data.Bases;

public abstract class BaseMappingConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    protected const string SeedJsonBasePath = "hexagonal.Data.SeedData";
    public abstract void Configure(EntityTypeBuilder<T> builder);
}