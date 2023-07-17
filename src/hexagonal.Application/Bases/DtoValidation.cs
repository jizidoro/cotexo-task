using FluentValidation;

namespace hexagonal.Application.Bases;

public class DtoValidation<TDto> : AbstractValidator<TDto>
    where TDto : EntityDto
{
}