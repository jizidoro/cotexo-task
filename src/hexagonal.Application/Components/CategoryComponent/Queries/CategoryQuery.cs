using AutoMapper;
using AutoMapper.QueryableExtensions;
using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.CategoryComponent.Contracts;
using hexagonal.Application.Paginations;
using hexagonal.Data;

namespace hexagonal.Application.Components.CategoryComponent.Queries;

public class CategoryQuery : ICategoryQuery
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _repository;

    public CategoryQuery(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IPageResultDto<CategoryDto>> GetAll(
        PaginationQuery? paginationQuery = null)
    {
        var paginationFilter = _mapper.Map<PaginationQuery?, PaginationFilter?>(paginationQuery);
        List<CategoryDto> list;
        if (paginationFilter == null)
        {
            list = await Task.Run(() => _repository.GetAllAsNoTracking()
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToList()).ConfigureAwait(false);

            return new PageResultDto<CategoryDto>(list);
        }

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        list = await Task.Run(() => _repository.GetAllAsNoTracking().Skip(skip)
            .Take(paginationFilter.PageSize)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new PageResultDto<CategoryDto>(paginationFilter, list);
    }

    public async Task<IListResultDto<CategoryDto>> GetAllDropdown()
    {
        var list = await Task.Run(() => _repository.GetAllAsNoTracking()
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new ListResultDto<CategoryDto>(list);
    }

    public async Task<ISingleResultDto<CategoryDto>> GetByIdDefault(int id)
    {
        var includes = new[] {"Category"};
        var entity = await _repository.GetById(id, includes).ConfigureAwait(false);
        var dto = _mapper.Map<CategoryDto>(entity);
        return new SingleResultDto<CategoryDto>(dto);
    }
}