using FluentValidation;
using hexagonal.Application.Bases;
using hexagonal.Application.Components.BookComponent.Contracts;

namespace hexagonal.Application.Components.BookComponent.Validations;

public class BookValidation<TDto> : DtoValidation<TDto>
    where TDto : BookDto
{
    protected void ValidateLivroe()
    {
        RuleFor(x => x.Livro)
            .NotEmpty().WithMessage("Nome do Livro is required")
            .Length(0, 50).WithMessage("Nome do Livro must be up to 50 characters long");
    }

    protected void ValidateAutor()
    {
        RuleFor(x => x.Autor)
            .NotEmpty().WithMessage("Autor do Livro is required")
            .Length(0, 50).WithMessage("Autor do Livro must be up to 50 characters long");
    }

    protected void ValidateTotalPaginas()
    {
        RuleFor(x => x.TotalPaginas)
            .GreaterThan(0).WithMessage("Total de Páginas must be greater than 0");
    }


    protected void ValidateCategoryId()
    {
        RuleFor(x => x.CategoryId).NotEqual(0).WithMessage("Category Id cannot be zero");
    }
    
}