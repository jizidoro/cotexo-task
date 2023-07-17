using hexagonal.Application.Bases;

namespace hexagonal.Application.Components.AuthenticationComponent.SecurityCore;

public interface IUcValidateLogin
{
    Task<SecurityResult> Execute(string email, string password);
}