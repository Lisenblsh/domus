using System;

namespace Domus.Models.Product;

public class ProductDto: IBaseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
