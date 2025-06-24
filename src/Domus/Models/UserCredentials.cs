using System;

namespace Domus.Models;

public class UserCredentials(string username, string password)
{
    public int Id { get; set; }
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}
