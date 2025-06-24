using System;

namespace Domus.Models;

public interface IBaseService<T> where T : IBaseDto
{
    public List<T> GetList();
    public T GetById(int id);
    public List<T> Find(string title);
    public bool Add(T entity);
    public bool Delete(int id);
    public bool Update(T entity);
}
