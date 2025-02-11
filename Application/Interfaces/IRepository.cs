using Domain.Interfaces;
using Ardalis.Specification;
using System.Linq.Expressions;



namespace Application.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetByIdsAsync(int[] Ids);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> InsertAsync(TEntity entity);
        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<TEntity?> DeleteAsync(int id);
        Task<TEntity?> DeleteAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetListBySpecAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetFirstBySpecAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetFirstBySpecAsync(ISpecification<TEntity> specification, bool tracking);
        Task SaveAsync();
    }
}