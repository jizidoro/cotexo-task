using hexagonal.Domain;

namespace hexagonal.Application.Components.BookComponent.Core.Validations;

public class BookEditValidation : IBookEditValidation
{
    public bool Execute(Book newRecord, Book? savedRecord)
    {
        return true;
    }
}