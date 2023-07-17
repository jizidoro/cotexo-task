using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.AuthenticationComponent.Contracts;

namespace hexagonal.Application.Components.AuthenticationComponent.Commands;

public interface IAuthenticationCommand
{
    Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto);
}