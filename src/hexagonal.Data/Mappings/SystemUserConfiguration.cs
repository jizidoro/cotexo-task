using hexagonal.Data.Bases;
using hexagonal.Data.Extensions;
using hexagonal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hexagonal.Data.Mappings;

public class SystemUserConfiguration : BaseMappingConfiguration<SystemUser>
{
    public override void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        builder.Property(b => b.Id).HasColumnName("syus_uuid_system_user").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_syus_system_user");

        builder.HasIndex(c => c.Email).HasDatabaseName("ix_un_syus_tx_email").IsUnique();
        builder.HasIndex(c => c.Registration).HasDatabaseName("ix_un_syus_tx_registration")
            .IsUnique();

        builder.InsertSeedData($"{SeedJsonBasePath}.system-user.json");
    }
}