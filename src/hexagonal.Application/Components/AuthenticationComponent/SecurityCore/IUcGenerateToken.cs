using hexagonal.Domain.Token;

namespace hexagonal.Application.Components.AuthenticationComponent.SecurityCore;

public interface IUcGenerateToken
{
    Task<string> Execute(TokenUser entity);
}