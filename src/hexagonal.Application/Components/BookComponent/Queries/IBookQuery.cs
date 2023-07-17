using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Contracts;

namespace hexagonal.Application.Components.BookComponent.Queries;

public interface IBookQuery
{
    Task<IListResultDto<BookDto>> GetAll();
    Task<ISingleResultDto<BookDto>> GetByIdDefault(int id);
}