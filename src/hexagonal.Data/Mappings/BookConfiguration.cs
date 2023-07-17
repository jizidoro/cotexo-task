using hexagonal.Data.Bases;
using hexagonal.Data.Extensions;
using hexagonal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hexagonal.Data.Mappings;

public class BookConfiguration : BaseMappingConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(b => b.Id).HasColumnName("book_uuid_book").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_book_book");

        builder.InsertSeedData($"{SeedJsonBasePath}.book.json");
    }
}