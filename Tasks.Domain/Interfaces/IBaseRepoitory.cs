using System.Linq.Expressions;

namespace Tasks.Domain.Interfaces;

public interface IBaseRepository<TEntity>
{
    Task AddAsync(TEntity item);

    Task UpdateAsync(TEntity item, dynamic updateFields = null);

    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> whereClause);

    Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> whereClause);
}