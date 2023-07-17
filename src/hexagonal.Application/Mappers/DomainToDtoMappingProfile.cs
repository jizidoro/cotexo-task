using AutoMapper;
using hexagonal.Application.Components.BookComponent.Contracts;
using hexagonal.Application.Components.CategoryComponent.Contracts;
using hexagonal.Domain;

namespace hexagonal.Application.Mappers;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<Category, CategoryDto>();
    }
}