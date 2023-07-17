using AutoMapper;
using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.AuthenticationComponent.Contracts;
using hexagonal.Application.Components.AuthenticationComponent.SecurityCore;

namespace hexagonal.Application.Components.AuthenticationComponent.Commands;

public class AuthenticationCommand : IAuthenticationCommand
{
    private readonly IMapper _mapper;
    private readonly IUcValidateLogin _validateLogin;


    public AuthenticationCommand(
        IUcValidateLogin validateLogin,
        IMapper mapper)
    {
        _validateLogin = validateLogin;
        _mapper = mapper;
    }

    public async Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto)
    {
        var result = await _validateLogin.Execute(dto.Email, dto.Password)
            .ConfigureAwait(false);

        if (!result.Success)
        {
            return new SingleResultDto<UserDto>(result);
        }

        var token = new UserDto
        {
            Token = result.TokenUser!.Token
        };

        return new SingleResultDto<UserDto>(token);
    }
}