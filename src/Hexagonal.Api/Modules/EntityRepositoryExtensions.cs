using hexagonal.Data;
using hexagonal.Data.Bases;
using hexagonal.Domain;

namespace hexagonal.API.Modules;

/// <summary>
///     Persistence Extensions.
/// </summary>
public static class EntityRepositoryExtensions
{
    /// <summary>
    ///     Add Persistence dependencies varying on configuration.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddEntityRepository(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISystemUserRepository, SystemUserRepository>();

        services.AddScoped<IRedisRepository<Book>, RedisRepository<Book>>();
        return services;
    }
}