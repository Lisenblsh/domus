using System;
using System.Text.Json.Serialization;

namespace Domus.Models.User;

public class UserCredentialsDto(string username):IBaseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = username;
    public string PasswordHash { get; set; } = null!;
    public int UserId { get; set; }
    [JsonIgnore]
    public UserDto User { get; set; } = null!;
}
