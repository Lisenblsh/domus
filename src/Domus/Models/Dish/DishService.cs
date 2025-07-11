using System;
using Microsoft.EntityFrameworkCore;

namespace Domus.Models.Dish;

public class DishService(NpgSqlContext db) : IBaseService<DishDto>
{
    private readonly NpgSqlContext _db = db;
    public List<DishDto> GetList()
    {
        return _db.Dishes.ToList();
    }

    public DishDto GetById(int id)
    {
        var dish = _db.Dishes.FirstOrDefault(dish => dish.Id == id);
        if (dish == null)
        {
            throw new NotImplementedException("no dish found");
        }
        return dish;
    }

    public List<DishDto> Find(string title)
    {
        return _db.Dishes.Where(dish => dish.Title == title).ToList();
    }

    public int Add(DishDto entity)
    {
        _db.Dishes.Add(entity);
        _db.SaveChanges();
        return entity.Id;
    }

    public bool Delete(int id)
    {
        _db.Dishes.Where(dish => dish.Id == id).ExecuteDelete();
        return true;
    }

    public bool Update(DishDto entity)
    {
        _db.Dishes.Update(entity);
        _db.SaveChanges();
        return true;
    }
}
