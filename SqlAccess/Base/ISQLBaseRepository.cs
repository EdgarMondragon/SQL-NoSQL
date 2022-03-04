using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SqlAccess.Base
{
	public interface ISQLBaseRepository<T> where T : class, IEntityBase, new()
	{
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateAllAsync(List<T> entities);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAllAsync(List<T> entities);
    }
}
