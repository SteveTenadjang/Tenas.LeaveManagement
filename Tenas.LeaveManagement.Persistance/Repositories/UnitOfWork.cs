using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TenasLeaveManagementDbContext _context;

        public UnitOfWork(TenasLeaveManagementDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }
        //public IGenericRepository<TEntity> GenericRepository => _genericRepository ??= new GenericRepository<T>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
