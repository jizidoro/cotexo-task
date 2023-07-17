using hexagonal.Data.DataAccess;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace hexagonal.API.Modules;

/// <summary>
///     Persistence Extensions.
/// </summary>
public static class PersistenceExtensions
{
    /// <summary>
    ///     Add Persistence dependencies varying on configuration.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static IServiceCollection AddSqlServer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var featureManager = services
            .BuildServiceProvider()
            .GetRequiredService<IFeatureManager>();


        services.AddDbContext<HexagonalContext>(options =>
            options.UseInMemoryDatabase("test_database").EnableSensitiveDataLogging()
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

        var context = services.BuildServiceProvider()
            .GetService<HexagonalContext>();
        HexagonalMemoryContextFake.AddDataFakeContext(context);


        return services;
    }
}