using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Core.Validations;
using hexagonal.Data;
using hexagonal.Domain;
using hexagonal.Domain.Bases;

namespace hexagonal.Application.Components.BookComponent.Core.UseCases;

public class UcBookEdit : UseCase, IUcBookEdit
{
    private readonly IBookEditValidation _bookEditValidation;
    private readonly IBookRepository _repository;

    public UcBookEdit(IBookEditValidation bookEditValidation, IBookRepository repository)
    {
        _bookEditValidation = bookEditValidation;
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Execute(Book newRecord)
    {
        var isValid = ValidateEntity(newRecord);
        if (!isValid.Success)
        {
            return isValid;
        }

        var savedRecord = await _repository.GetById(newRecord.Id).ConfigureAwait(false);

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

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Update(obj);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

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