using hexagonal.Domain;

namespace hexagonal.Application.Components.BookComponent.Core.Validations;

public interface IBookCreateValidation
{
    bool Execute(Book entity);
}