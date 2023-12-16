using System.Linq.Expressions;

namespace Tenas.LeaveManagement.Application.Contracts.Persistance
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table { get; }
        Task<IReadOnlyList<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task<TEntity> Add(TEntity entity);
        Task AddRange(List<TEntity> entities);
        Task Delete(Guid id);
        Task Update(TEntity entity);
        Task<bool> Exists(Guid id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
        Task<IReadOnlyList<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

    }
}
