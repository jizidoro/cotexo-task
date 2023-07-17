using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Contracts;

namespace hexagonal.Application.Components.BookComponent.Commands;

public interface IBookCommand
{
    Task<ISingleResultDto<EntityDto>> Create(BookCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(BookEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(int id);
}