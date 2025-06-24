using System;
using Microsoft.EntityFrameworkCore;

namespace Domus.Models.Product;

public class ProductService(NpgSlqContext db) : IBaseService<ProductDto>
{
    private readonly NpgSlqContext _db = db;
    public List<ProductDto> GetList()
    {
        return _db.Products.ToList();
    }

    public ProductDto GetById(int id)
    {
        var product = _db.Products.FirstOrDefault(product => product.Id == id);
        if (product == null)
        {
            throw new NotImplementedException("no product found");
        }
        return product;
    }

    public List<ProductDto> Find(string title)
    {
        return _db.Products.Where(product => product.Title == title).ToList();
    }

    public bool Add(ProductDto entity)
    {
        _db.Products.Add(entity);
        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        _db.Recipes.Where(product => product.Id == id).ExecuteDelete();
        return true;
    }

    public bool Update(ProductDto entity)
    {
        _db.Products.Update(entity);
        _db.SaveChanges();
        return true;
    }
}
