using hexagonal.Domain;

namespace hexagonal.Application.Components.BookComponent.Core.Validations;

public interface IBookDeleteValidation
{
    bool Execute(Book entity);
}