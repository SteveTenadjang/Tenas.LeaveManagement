using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tenas.LeaveManagement.Application.Exceptions;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TenasLeaveManagementDbContext _context;
        private readonly DbSet<T> _dbSet;

        public virtual IQueryable<T> Table => _context.Set<T>();

        public GenericRepository(TenasLeaveManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IReadOnlyList<T>> GetAll()
            => await _dbSet.ToListAsync();

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(Guid id)
            => await _dbSet.FindAsync(id) ?? throw new NotFoundException(nameof(T), id);

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
            => await GetById(id) == null;

        public async Task<IReadOnlyList<T>> Find(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();
    }
}
