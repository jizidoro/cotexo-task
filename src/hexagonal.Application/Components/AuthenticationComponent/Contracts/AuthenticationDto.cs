namespace hexagonal.Application.Components.AuthenticationComponent.Contracts;

public class AuthenticationDto
{
    public AuthenticationDto()
    {
        Password = "";
    }

    public int Key { get; set; }
    public string Password { get; set; }
}