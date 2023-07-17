using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Core.Validations;
using hexagonal.Data;
using hexagonal.Data.Bases;
using hexagonal.Domain;
using hexagonal.Domain.Bases;

namespace hexagonal.Application.Components.BookComponent.Core.UseCases;

public class UcBookCreate : UseCase, IUcBookCreate
{
    private readonly IBookCreateValidation _bookCreateValidation;
    private readonly IRedisRepository<Book> _redisRepository;

    public UcBookCreate(IBookCreateValidation bookCreateValidation, IRedisRepository<Book> redisRepository)
    {
        _bookCreateValidation = bookCreateValidation;
        _redisRepository = redisRepository;
    }

    public async Task<ISingleResult<Entity>> Execute(Book newRecord)
    {
        var isValid = ValidateEntity(newRecord);
        if (!isValid.Success)
        {
            return isValid;
        }

        var validate = _bookCreateValidation.Execute(newRecord);
        if (!validate)
        {
            return new ErrorResult<Entity>();
        }

        await _redisRepository.Add(newRecord).ConfigureAwait(false);

        return new CreateResult<Entity>(true,
            "");
    }

    
}