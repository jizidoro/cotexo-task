using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Core.Validations;
using hexagonal.Data;
using hexagonal.Data.Bases;
using hexagonal.Domain;
using hexagonal.Domain.Bases;

namespace hexagonal.Application.Components.BookComponent.Core.UseCases;

public class UcBookDelete : UseCase, IUcBookDelete
{
    private readonly IBookDeleteValidation _bookDeleteValidation;
    private readonly IRedisRepository<Book> _redisRepository;

    public UcBookDelete(IBookDeleteValidation bookDeleteValidation, IRedisRepository<Book> redisRepository)
    {
        _bookDeleteValidation = bookDeleteValidation;
        _redisRepository = redisRepository;
    }

    public async Task<ISingleResult<Entity>> Execute(int id)
    {
        var entity = new Book {Id = id};

        var savedRecord = await _redisRepository.GetById(entity.Id).ConfigureAwait(false);

        if (savedRecord is null)
        {
            return new ErrorResult<Entity>();
        }

        var validate = _bookDeleteValidation.Execute(savedRecord);
        if (!validate)
        {
            return new ErrorResult<Entity>(false, "Livro esta ativo");
        }

        var bookId = savedRecord.Id;
        _redisRepository.Remove(bookId);
        
        return new DeleteResult<Entity>(true,
            "");
    }
}