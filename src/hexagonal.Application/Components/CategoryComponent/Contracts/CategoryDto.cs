using hexagonal.Application.Bases;

namespace hexagonal.Application.Components.CategoryComponent.Contracts;

public class CategoryDto : EntityDto
{
    public string? Name { get; set; }
}