using System;

namespace Domus.Models.User;

public class UserDto: IBaseDto
{
    public int Id { get; set; }
    public string Name { get; set;} = string.Empty;
    public UserCredentialsDto? Credentials { get; set; }
}
