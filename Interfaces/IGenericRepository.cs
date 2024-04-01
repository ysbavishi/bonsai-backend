using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

public interface IGenericRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T t);
    void AddRange(IEnumerable<T> t);
    void Remove(T t);
    void RemoveRange(IEnumerable<T> t);
}