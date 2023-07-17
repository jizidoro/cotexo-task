using System.Reflection;
using hexagonal.Domain.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hexagonal.Data.Extensions;

public static class EntityBuilderUtilities
{
    public static void InsertSeedData<TEntity>(
        this EntityTypeBuilder<TEntity> builder, string seedJsonPath) where TEntity : Entity
    {
        var assembly = Assembly.GetAssembly(typeof(JsonUtilities));
        var entities =
            JsonUtilities.GetListFromJson<TEntity>(assembly?.GetManifestResourceStream(seedJsonPath));
        var hydratedEntities = HydrateValues(entities);

        if (hydratedEntities != null) builder.HasData(hydratedEntities);
    }

    private static IEnumerable<TEntity>? HydrateValues<TEntity>(IEnumerable<TEntity>? entities) where TEntity : Entity
    {
        return entities;
    }
}