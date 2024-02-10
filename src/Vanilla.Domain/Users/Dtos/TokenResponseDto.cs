namespace Vanilla.Domain.Users.Dtos;

public class TokenResponseDto
{
    public bool Authenticated { get; set; }
    public string Created { get; set; }
    public string Expiration { get; set; }
    public string AccessToken { get; set; }
}
