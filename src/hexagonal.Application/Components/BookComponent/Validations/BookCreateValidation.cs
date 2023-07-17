using hexagonal.Application.Components.BookComponent.Contracts;

namespace hexagonal.Application.Components.BookComponent.Validations;

public class BookCreateValidation : BookValidation<BookCreateDto>
{
    public BookCreateValidation()
    {
        ValidateLivroe();
        ValidateAutor();
        ValidateTotalPaginas();
        ValidateCategoryId();
    }
}