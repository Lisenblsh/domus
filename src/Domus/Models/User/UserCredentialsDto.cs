using System;

namespace Domus.Models.User;

public class UserCredentialsDto(string username, string password):IBaseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;
}
