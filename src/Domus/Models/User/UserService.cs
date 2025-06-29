using System;
using System.Text;
using Domus.FrontendModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domus.Models.User;

public class UserService(NpgSqlContext db, IPasswordHasher<UserCredentialsDto> passwordHasher): IBaseService<UserDto>
{
    public readonly NpgSqlContext _db = db;
    public readonly IPasswordHasher<UserCredentialsDto> _passwordHasher = passwordHasher;

    public bool CheckUserAccess(int? id)
    {
        if (id == null || !_db.Users.Where(user => user.Id == id).Any())
        {
            throw new UnauthorizedAccessException("no access");
        }
        else
        {
            return true;
        }
    }

    private bool CheckUser(string username)
    {
        return _db.UserCredentials.Where(user => user.Username == username).Any();
    }

    public int CreateUser(CreateUserDto userData)
    {
        if (CheckUser(userData.Username))
            throw new NotImplementedException("user in db");

        var credentials = new UserCredentialsDto(userData.Username);

        credentials.PasswordHash = _passwordHasher.HashPassword(credentials, userData.Password);

        var user = new UserDto
        {
            Name = userData.Name,
            Credentials = credentials
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        return user.Id;
    }

    public int ValidateUser(FrontendModels.UserCredentialsDto userData)
    {
        var user = _db.UserCredentials.FirstOrDefault(user => user.Username == userData.Username);
        if (user == null)
            throw new NotImplementedException("user == null");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userData.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new NotImplementedException("user validate fail");

        return user.Id;
    }

    public List<UserDto> GetList()
    {
        return _db.Users.ToList();
    }

    public UserDto GetById(int id)
    {
        var user = _db.Users.FirstOrDefault(user => user.Id == id);
        if (user == null)
        {
            throw new NotImplementedException("no recipe found");
        }
        return user;
    }

    public List<UserDto> Find(string title)
    {
        return _db.Users.Where(user => user.Name == title).ToList();
    }

    public int Add(UserDto entity)
    {
        throw new NotSupportedException("instead this use CreateUser(..)");
    }

    public bool Delete(int id)
    {
        _db.Recipes.Where(user => user.Id == id).ExecuteDelete();
        return true;
    }

    public bool Update(UserDto entity)
    {
        _db.Users.Update(entity);
        _db.SaveChanges();
        return true;
    }
}
