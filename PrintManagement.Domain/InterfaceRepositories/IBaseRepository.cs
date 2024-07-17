using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.InterfaceRepositories
{
	public interface IBaseRepository<TEntity> where TEntity : class
	{
		Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
		Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
		Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
		Task DeleteAsync(Guid id, CancellationToken cancellationToken);
		Task DeleteAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
		Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
		Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<int> CountAsync(string include, Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
	}
}
