using System;

namespace Domus.FrontendModels;

public class UserCredentialsDto(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}
