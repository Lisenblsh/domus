using System;
using Microsoft.EntityFrameworkCore;

namespace Domus.Models.Menu;

public class MenuService(NpgSqlContext db) : IBaseService<MenuDto>
{
    private readonly NpgSqlContext _db = db;

    public List<MenuDto> GetList()
    {
        return _db.Menus.ToList();
    }

    public MenuDto GetById(int id)
    {
        var menu = _db.Menus.FirstOrDefault(menu => menu.Id == id);
        if (menu == null)
        {
            throw new NotImplementedException("no menu found");
        }
        return menu;
    }

    public List<MenuDto> Find(string title)
    {
        return _db.Menus.Where(menu => menu.Title == title).ToList();
    }

    public int Add(MenuDto entity)
    {
        _db.Menus.Add(entity);
        _db.SaveChanges();
        return entity.Id;
    }

    public bool Delete(int id)
    {
        _db.Menus.Where(menu => menu.Id == id).ExecuteDelete();
        return true;
    }

    public bool Update(MenuDto entity)
    {
        _db.Menus.Update(entity);
        _db.SaveChanges();
        return true;
    }
}
