using System;
using Domus.Models.Recipe;

namespace Domus.Models.Dish;

public class DishDto: IBaseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public List<RecipeDto> Recipes { get; set; } = [];
}
