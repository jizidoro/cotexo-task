using System.Text.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Contracts;
using hexagonal.Data;
using hexagonal.Data.Bases;
using hexagonal.Domain;
using StackExchange.Redis;

namespace hexagonal.Application.Components.BookComponent.Queries;

public class BookQuery : IBookQuery
{
    private readonly IMapper _mapper;
    private readonly IRedisRepository<Book> _redisRepository;
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public BookQuery(IMapper mapper, IConnectionMultiplexer connectionMultiplexer, IRedisRepository<Book> redisRepository)
    {
        _mapper = mapper;
        _connectionMultiplexer = connectionMultiplexer;
        _redisRepository = redisRepository;
    }

    public async Task<IListResultDto<BookDto>> GetAll()
    {
        // get all keys from Redis
        var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
        var keys = server.Keys();

        var books = new List<Book>();
        foreach (var key in keys)
        {
            var book = await _redisRepository.GetById(int.Parse(key));
            if (book != null)
            {
                books.Add(book);
            }
        }

        // map to DTOs
        var list = _mapper.Map<List<BookDto>>(books);
        return new ListResultDto<BookDto>(list);
    }

    public async Task<ISingleResultDto<BookDto>> GetById(int id)
    {
        // Fetch the data from Redis
        var book = await _redisRepository.GetById(id);
        
        // Then map to the DTO
        var dto = _mapper.Map<BookDto>(book);

        // And return the result
        return new SingleResultDto<BookDto>(dto);
    }
}