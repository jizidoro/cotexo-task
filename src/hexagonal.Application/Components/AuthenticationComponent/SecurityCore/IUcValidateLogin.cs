using hexagonal.Application.Bases;

namespace hexagonal.Application.Components.AuthenticationComponent.SecurityCore;

public interface IUcValidateLogin
{
    Task<SecurityResult> Execute(int key, string password);
}