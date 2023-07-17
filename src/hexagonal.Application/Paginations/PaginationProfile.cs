using AutoMapper;

namespace hexagonal.Application.Paginations;

public class PaginationProfile : Profile
{
    public PaginationProfile()
    {
        CreateMap<PaginationQuery, PaginationFilter>();
    }
}