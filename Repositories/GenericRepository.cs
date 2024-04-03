using System;
using System.Data;
using System.Linq.Expressions;
using BonsaiBackend.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore;

public class GenericeRepository<T> : IGenericRepository<T> where T:class {
    protected readonly DBBonsaiContext _context;
    public GenericeRepository(DBBonsaiContext context) {
        this._context = context;
    }

    public bool CompleteChanges() {
        try {
            _context.SaveChanges();
            return true;
        } catch (Exception err) {
            Console.WriteLine(err);
            return false;
        }
    }

    public T GetById(int id) {
        return _context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll() {
        return _context.Set<T>().ToList();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression) {
        return _context.Set<T>().Where(expression);
    }

    public void Update(T t) {
        // T existingEntity = _context.Set<T>().Find(id);
        _context.Entry(t).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Add(T t) {
        _context.Set<T>().Add(t);
        _context.SaveChanges();
    }

    public void AddRange(IEnumerable<T> t) {
        _context.Set<T>().AddRange(t);
    }

    public void Remove(T t) {
        _context.Set<T>().Remove(t);
    }

    public void RemoveRange(IEnumerable<T> t) {
        _context.Set<T>().RemoveRange(t);
    }
}