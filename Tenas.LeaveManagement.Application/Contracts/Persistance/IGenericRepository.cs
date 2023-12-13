using System.Linq.Expressions;

namespace Tenas.LeaveManagement.Application.Contracts.Persistance
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task Delete(Guid id);
        Task Update(T entity);
        Task<bool> Exists(Guid id);
        Task<IReadOnlyList<T>> Find(Expression<Func<T, bool>> predicate);

    }
}
