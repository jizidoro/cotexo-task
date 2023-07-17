using hexagonal.Application.Bases.Interfaces;
using hexagonal.Domain;

namespace hexagonal.Application.Components.AuthenticationComponent.SecurityCore.Validation;

public interface ISystemUserPasswordValidation
{
    ISingleResult<SystemUser> Execute(string email, string password);
}