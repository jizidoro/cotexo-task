using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.CategoryComponent.Contracts;
using hexagonal.Application.Paginations;

namespace hexagonal.Application.Components.CategoryComponent.Queries;

public interface ICategoryQuery
{
    Task<IPageResultDto<CategoryDto>> GetAll(PaginationQuery? paginationQuery = null);
    Task<IListResultDto<CategoryDto>> GetAllDropdown();
    Task<ISingleResultDto<CategoryDto>> GetByIdDefault(int id);
}