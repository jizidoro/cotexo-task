using hexagonal.API.Modules.Common.FeatureFlags;
using hexagonal.Application.Bases;
using hexagonal.Application.Components.AuthenticationComponent.Commands;
using hexagonal.Application.Components.AuthenticationComponent.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace hexagonal.API.Controllers;

[FeatureGate(CustomFeature.Authentication)]
[AllowAnonymous]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticationCommand _authenticationCommand;

    public TokenController(IAuthenticationCommand authenticationCommand)
    {
        _authenticationCommand = authenticationCommand;
    }


    [HttpPost("generate-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SingleResultDto<EntityDto>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GenerateToken([FromBody] AuthenticationDto dto)
    {
        try
        {
            var result = await _authenticationCommand.GenerateToken(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}