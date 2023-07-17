using hexagonal.Domain;

namespace hexagonal.Application.Components.BookComponent.Core.Validations;

public class BookCreateValidation : IBookCreateValidation
{
    public bool Execute(Book entity)
    {
        return true;
    }
}