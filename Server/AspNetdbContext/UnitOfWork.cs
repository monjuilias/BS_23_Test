using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetdbContext
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;
        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork(AspDBContext context)
        {
            _context = context;
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> Repository<T>() where T : class, new()
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type))
                return (IGenericRepository<T>)_repositories[type];

            var repositoryType = typeof(IGenericRepository<>);
            if (repositoryType.IsGenericType)
            {

            }
            object[] paramsArray = new object[] { _context };
            var instance = Activator.CreateInstance(typeof(GenericRepository<T>), paramsArray);
            _repositories.Add(type, instance);

            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
    public interface IUnitOfWork : IUnitOfWorkForService
    {
        Task<int> Save();
        void SaveChanges();
        void Dispose(bool disposing);
        void Dispose();
    }

    public interface IUnitOfWorkForService
    {
        IGenericRepository<T> Repository<T>() where T : class, new();

    }
}
