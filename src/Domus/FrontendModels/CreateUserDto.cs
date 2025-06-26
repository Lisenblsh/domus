using System;

namespace Domus.FrontendModels;

public class CreateUserDto(string name, string username, string password)
{
    public string Name { get; set; } = name;
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}
