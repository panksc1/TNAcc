using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddEntityAsync(T entity);
        Task<T> DeleteEntityAsync(int id);
        Task<T> UpdateEntityAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}