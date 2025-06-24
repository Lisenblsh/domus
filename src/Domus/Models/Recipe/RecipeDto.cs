using System;
using Domus.Models.Product;

namespace Domus.Models.Recipe;

public class RecipeDto: IBaseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public IEnumerable<ProductDto> Products { get; set; } = [];
}
