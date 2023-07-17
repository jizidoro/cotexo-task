using hexagonal.Domain;

namespace hexagonal.Application.Components.BookComponent.Core.Validations;

public interface IBookEditValidation
{
    bool Execute(Book newRecord, Book? savedRecord);
}