using hexagonal.Data.Bases;
using hexagonal.Data.Extensions;
using hexagonal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hexagonal.Data.Mappings;

public class CategoryConfiguration : BaseMappingConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(b => b.Id).HasColumnName("cate_uuid_category").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_cate_category");

        builder.InsertSeedData($"{SeedJsonBasePath}.category.json");
    }
}