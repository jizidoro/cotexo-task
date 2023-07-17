using hexagonal.Application.Bases;

namespace hexagonal.Application.Components.BookComponent.Contracts;

public class BookDto : EntityDto
{
    public string? Livro { get; set; }

    public string? Autor { get; set; }

    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public int? TotalPaginas { get; set; }

    public bool? IsActive { get; set; }
}