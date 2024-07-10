using Microsoft.EntityFrameworkCore;
using PrintManagement.Domain.InterfaceRepositories;
using PrintManagement.Infrastructure.Database.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Infrastructure.ImplementRepositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		protected IDbContext _IDbContext = null;
		protected DbSet<TEntity> _dbset;
		protected DbContext _dbContext;
		protected DbSet<TEntity> DBSet
		{
			get
			{
				if (_dbset == null)
				{
					_dbset = _dbContext.Set<TEntity>() as DbSet<TEntity>;
				}
				return _dbset;
			}
		}
		public BaseRepository(IDbContext DbContext)
		{
			_IDbContext = DbContext;
			_dbContext = (DbContext)DbContext;
		}
		public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
		{
			IQueryable<TEntity> query = expression != null ? DBSet.Where(expression) : DBSet;
			return await query.CountAsync(cancellationToken);
		}

		public async Task<int> CountAsync(string include, Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
		{
			IQueryable<TEntity> query;
			if (!string.IsNullOrEmpty(include))
			{
				query = BuildQueryable(new List<string> { include }, expression);
				return await query.CountAsync(cancellationToken);
			}
			return await CountAsync(expression, cancellationToken);
		}
		protected IQueryable<TEntity> BuildQueryable(List<string> includes, Expression<Func<TEntity, bool>> expression)
		{
			IQueryable<TEntity> query = DBSet.AsQueryable();
			if (expression != null)
			{
				query = query.Where(expression);
			}
			if (includes != null && includes.Count > 0)
			{
				foreach (string include in includes)
				{
					query = query.Include(include.Trim());
				}
			}
			return query;
		}
		public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			await DBSet.AddAsync(entity);
			await _IDbContext.CommitChangesAsync(cancellationToken);
			return entity;
		}

		public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
		{
			await DBSet.AddRangeAsync(entities);
			await _IDbContext.CommitChangesAsync(cancellationToken);
			return entities;
		}

		public async Task DeleteAsync(int id, CancellationToken cancellationToken)
		{
			var model = await DBSet.FindAsync(id);
			if (model != null)
			{
				DBSet.Remove(model);
				await _IDbContext.CommitChangesAsync(cancellationToken);
			}
		}

		public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
		{
			IQueryable<TEntity> query = expression != null ? DBSet.Where(expression) : DBSet;
            var dataQuery = query;
            if (dataQuery != null)
            {
                DBSet.RemoveRange(dataQuery);
                await _IDbContext.CommitChangesAsync(cancellationToken);
            }
		}

		public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
		{
			IQueryable<TEntity> query = expression != null ? DBSet.Where(expression) : DBSet;
			return query.AsNoTracking();
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
		{
			return await DBSet.FirstOrDefaultAsync(expression, cancellationToken);
		}

		public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await DBSet.FindAsync(id, cancellationToken);
		}

		public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
		{
			return await DBSet.FirstOrDefaultAsync(expression, cancellationToken);
		}

		public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _IDbContext.CommitChangesAsync(cancellationToken);
			return entity;
		}

		public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
		{
			foreach (var entity in entities)
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
			}
			await _IDbContext.CommitChangesAsync(cancellationToken);
			return entities;
		}
	}
}
