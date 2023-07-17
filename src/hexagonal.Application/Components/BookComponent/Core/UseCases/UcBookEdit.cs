using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Core.Validations;
using hexagonal.Data;
using hexagonal.Data.Bases;
using hexagonal.Domain;
using hexagonal.Domain.Bases;

namespace hexagonal.Application.Components.BookComponent.Core.UseCases;

public class UcBookEdit : UseCase, IUcBookEdit
{
    private readonly IBookEditValidation _bookEditValidation;
    private readonly IRedisRepository<Book> _redisRepository;

    public UcBookEdit(IBookEditValidation bookEditValidation, IRedisRepository<Book> redisRepository)
    {
        _bookEditValidation = bookEditValidation;
        _redisRepository = redisRepository;
    }

    public async Task<ISingleResult<Entity>> Execute(Book newRecord)
    {
        var isValid = ValidateEntity(newRecord);
        if (!isValid.Success)
        {
            return isValid;
        }

        var savedRecord = await _redisRepository.GetById(newRecord.Id).ConfigureAwait(false);

        if (savedRecord is null)
        {
            return new ErrorResult<Entity>();
        }

        var validate = _bookEditValidation.Execute(newRecord, savedRecord);
        if (!validate)
        {
            return new ErrorResult<Entity>();
        }

        var obj = savedRecord;
        HydrateValues(obj, newRecord);
        
        _redisRepository.Update(obj);

        return new EditResult<Entity>(true,
            "");
    }

    private static void HydrateValues(Book target, Book source)
    {
        target.Livro = source.Livro;
        target.Autor = source.Autor;
        target.CategoryId = source.CategoryId;
        target.TotalPaginas = source.TotalPaginas;
        target.IsActive = source.IsActive;
    }
}