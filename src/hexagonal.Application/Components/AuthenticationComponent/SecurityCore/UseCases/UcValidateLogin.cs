using hexagonal.Application.Bases;
using hexagonal.Application.Components.AuthenticationComponent.SecurityCore.Validation;
using hexagonal.Domain.Token;

namespace hexagonal.Application.Components.AuthenticationComponent.SecurityCore.UseCases;

public class UcValidateLogin : IUcValidateLogin
{
    private readonly IUcGenerateToken _generateToken;
    private readonly ISystemUserPasswordValidation _systemUserPasswordValidation;


    public UcValidateLogin(
        ISystemUserPasswordValidation systemUserPasswordValidation,
        IUcGenerateToken generateToken)
    {
        _systemUserPasswordValidation = systemUserPasswordValidation;
        _generateToken = generateToken;
    }

    public async Task<SecurityResult> Execute(string email, string password)
    {
        var result = await Task.Run(async () =>
        {
            var resultPassword = _systemUserPasswordValidation.Execute(email, password);

            if (resultPassword.Success)
            {
                var selectedUser = resultPassword.Data!;

                var roles = new List<string> { "Role" };

                var user = new TokenUser
                {
                    Id = selectedUser.Id,
                    Name = selectedUser.Name,
                    Token = "",
                    Roles = roles
                };
                user.Token = await _generateToken.Execute(user).ConfigureAwait(false);

                return new SecurityResult(user);
            }

            return new SecurityResult(resultPassword.Code, resultPassword.Message);
        }).ConfigureAwait(false);

        return result;
    }
}