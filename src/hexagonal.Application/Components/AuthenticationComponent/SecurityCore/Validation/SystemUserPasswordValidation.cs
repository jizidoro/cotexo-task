using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Data;
using hexagonal.Domain;
using hexagonal.Domain.Extensions;

namespace hexagonal.Application.Components.AuthenticationComponent.SecurityCore.Validation;

public class SystemUserPasswordValidation : ISystemUserPasswordValidation
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISystemUserRepository _systemUserRepository;

    public SystemUserPasswordValidation(ISystemUserRepository systemUserRepository,
        IPasswordHasher passwordHasher)
    {
        _systemUserRepository = systemUserRepository;
        _passwordHasher = passwordHasher;
    }

    public ISingleResult<SystemUser> Execute(string email, string password)
    {
        var usuSession = _systemUserRepository.GetByPredicate(user => user.Email == email).Result;
        var hasUser = usuSession != null;

        if (!hasUser)
            return new SingleResult<SystemUser>(1001,
                "Usuário ou password informados não são válidos");

        var (verified, needsUpgrade) = _passwordHasher.Check(usuSession!.Password, password);

        if (!verified)
        {
            return new SingleResult<SystemUser>(1001,
                "Usuário ou password informados não são válidos");
        }

        if (needsUpgrade)
        {
            return new SingleResult<SystemUser>(1009,
                "Senha precisa ser atualizada");
        }

        return new SingleResult<SystemUser>(usuSession);
    }
}