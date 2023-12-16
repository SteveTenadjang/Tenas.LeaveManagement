namespace Tenas.LeaveManagement.Application.Contracts.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        Task Save();
    }
}
