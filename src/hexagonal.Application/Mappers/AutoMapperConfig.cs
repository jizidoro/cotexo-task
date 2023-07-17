using AutoMapper;
using hexagonal.Application.Paginations;

namespace hexagonal.Application.Mappers;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DomainToDtoMappingProfile());
            cfg.AddProfile(new DtoToDomainMappingProfile());
            cfg.AddProfile(new PaginationProfile());
        });
    }
}