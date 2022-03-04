using Entities.Base;
using SqlAccess.Connection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace SqlAccess.Base
{
	public class SQLBaseRepository<T> : ISQLBaseRepository<T>
	   where T : class, IEntityBase, new()
	{
		IConnectionFactory connectionFactory;
		public SQLBaseRepository(IConnectionFactory connectionFactory)
		{
			this.connectionFactory = connectionFactory;
		}

		public async Task<bool> DeleteAllAsync(List<T> entities)
		{
			bool isSuccess = false;
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();
				isSuccess = await connection.DeleteAsync(entities);
				connection.Close();
			}
			return isSuccess;
		}

		public async Task<bool> DeleteAsync(T entity)
		{
			bool isSuccess = false;
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();
				isSuccess = await connection.DeleteAsync(entity);
				connection.Close();
			}

			return isSuccess;
		}

		public async Task<IEnumerable<T>> FindByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{
			List<T> entities = new List<T>();
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();
				var allEntities = await connection.GetAllAsync<T>();
				allEntities = allEntities.ToList();
				entities.AddRange(allEntities.AsQueryable().Where(predicate).Select(x => x));
				connection.Close();
			}
			return entities;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			List<T> entities = new List<T>();
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();
				var result = await connection.GetAllAsync<T>();
				entities = result.ToList();
				connection.Close();
			}
			return entities;
		}

		public async Task<T> GetAsync(int id)
		{
			T entity = new T();
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();

				entity = await connection.GetAsync<T>(id);
				connection.Close();

			}
			return entity;
		}

		public async Task<T> GetByAsync(Expression<Func<T, bool>> predicate)
		{
			T entity = new T();
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();
				var allEntities = await connection.GetAllAsync<T>();
				allEntities = allEntities.ToList();
				entity = allEntities.AsQueryable().Where(predicate).Select(x => x).FirstOrDefault();
				connection.Close();
			}
			return entity;
		}

		public async Task<T> InsertAsync(T entity)
		{
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();
				var Id = await connection.InsertAsync(entity);
				entity.Id = Id;
				connection.Close();
			}
			return entity;
		}

		public async Task<bool> UpdateAllAsync(List<T> entities)
		{
			bool isSuccess = false;
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();
				isSuccess = await connection.UpdateAsync(entities);
				connection.Close();
			}
			return isSuccess;
		}

		public async Task<bool> UpdateAsync(T entity)
		{
			bool isSuccess = false;
			using (var connection = connectionFactory.GetConnection)
			{
				connection.Open();
				isSuccess = await connection.UpdateAsync(entity);
				connection.Close();
			}
			return isSuccess;
		}
	}
}
