using AutoMapper;
using AutoMapper.QueryableExtensions;
using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Contracts;
using hexagonal.Data;

namespace hexagonal.Application.Components.BookComponent.Queries;

public class BookQuery : IBookQuery
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _repository;

    public BookQuery(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IListResultDto<BookDto>> GetAll()
    {
        var list = await Task.Run(() => _repository.GetAllAsNoTracking()
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .ToList()).ConfigureAwait(false);

        return new ListResultDto<BookDto>(list);
    }

    public async Task<ISingleResultDto<BookDto>> GetByIdDefault(int id)
    {
        var includes = new[] {"Category"};
        var entity = await _repository.GetById(id, includes).ConfigureAwait(false);
        var dto = _mapper.Map<BookDto>(entity);
        return new SingleResultDto<BookDto>(dto);
    }
}