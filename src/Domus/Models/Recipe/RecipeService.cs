using System;
using Microsoft.EntityFrameworkCore;

namespace Domus.Models.Recipe;

public class RecipeService(NpgSlqContext db) : IBaseService<RecipeDto>
{
    private readonly NpgSlqContext _db = db;

    public List<RecipeDto> GetList()
    {
        return _db.Recipes.ToList();
    }

    public RecipeDto GetById(int id)
    {
        var recipe = _db.Recipes.FirstOrDefault(recipe => recipe.Id == id);
        if (recipe == null)
        {
            throw new NotImplementedException("no recipe found");
        }
        return recipe;
    }

    public List<RecipeDto> Find(string title)
    {
        return _db.Recipes.Where(recipe => recipe.Title == title).ToList();
    }

    public bool Add(RecipeDto entity)
    {
        _db.Recipes.Add(entity);
        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        _db.Recipes.Where(recipe => recipe.Id == id).ExecuteDelete();
        return true;
    }

    public bool Update(RecipeDto entity)
    {
        _db.Recipes.Update(entity);
        _db.SaveChanges();
        return true;
    }
}
