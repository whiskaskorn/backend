namespace backend.Api.UserDtos;

public class UserPostDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public UserPostDTO(string? username, string? email, string? password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}