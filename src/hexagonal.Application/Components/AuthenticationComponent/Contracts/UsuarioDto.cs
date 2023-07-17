using hexagonal.Application.Bases;

namespace hexagonal.Application.Components.AuthenticationComponent.Contracts;

public class UserDto : EntityDto
{
    public string? Token { get; set; }
}