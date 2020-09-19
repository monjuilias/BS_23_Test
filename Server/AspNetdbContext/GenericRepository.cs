using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AspNetdbContext
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AspDBContext _context;
        public GenericRepository(AspDBContext context)
        {
            _context = context;
        }
        public T Add(T entity)
        {
           return _context.Set<T>().Add(entity).Entity;
        
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public T Edit(T entity)
        {
            return _context.Set<T>().Update(entity).Entity;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
