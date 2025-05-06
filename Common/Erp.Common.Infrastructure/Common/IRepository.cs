using Erp.Common.Domain.Common;

namespace Erp.Common.Infrastructure.Common;

public interface IRepository<T> where T : Entity
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
} 