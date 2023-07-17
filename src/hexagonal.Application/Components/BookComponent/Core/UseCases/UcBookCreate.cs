using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Core.Validations;
using hexagonal.Data;
using hexagonal.Domain;
using hexagonal.Domain.Bases;

namespace hexagonal.Application.Components.BookComponent.Core.UseCases;

public class UcBookCreate : UseCase, IUcBookCreate
{
    private readonly IBookCreateValidation _bookCreateValidation;
    private readonly IBookRepository _repository;

    public UcBookCreate(IBookCreateValidation bookCreateValidation, IBookRepository repository)
    {
        _bookCreateValidation = bookCreateValidation;
        _repository = repository;
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

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        await _repository.Add(newRecord).ConfigureAwait(false);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        return new CreateResult<Entity>(true,
            "");
    }
}