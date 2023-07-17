namespace hexagonal.Application.Components.AuthenticationComponent.Contracts;

public class AuthenticationDto
{
    public AuthenticationDto()
    {
        Password = "";
    }

    public string Email { get; set; }
    public string Password { get; set; }
}