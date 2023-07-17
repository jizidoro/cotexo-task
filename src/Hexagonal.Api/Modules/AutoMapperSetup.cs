using hexagonal.Application.Mappers;

namespace hexagonal.API.Modules;

/// <summary>
///     AutoMapperSetup
/// </summary>
public static class AutoMapperSetup
{
    /// <summary>
    ///     AddAutoMapperSetup
    /// </summary>
    /// <param name="services"></param>
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(DomainToDtoMappingProfile),
            typeof(DtoToDomainMappingProfile));
    }
}