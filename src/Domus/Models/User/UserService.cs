using System;
using Microsoft.EntityFrameworkCore;

namespace Domus.Models.User;

public class UserService(NpgSlqContext db): IBaseService<UserDto>
{
    public readonly NpgSlqContext _db = db;

    public bool CheckUser(int? id)
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

    public bool Add(UserDto entity)
    {
        _db.Users.Add(entity);
        _db.SaveChanges();
        return true;
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
