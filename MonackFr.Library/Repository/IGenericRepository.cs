using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace MonackFr.Repository
{
	/// <summary>
	/// Interface for generic repository. Necessary for unit test
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IGenericRepository<T> where T : class
	{
		/// <summary>
		/// Get all entities
		/// </summary>
		/// <returns></returns>
		IQueryable<T> GetAll();

		/// <summary>
		/// Get entity
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		T GetSingle(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Find entity
		/// </summary>
		/// <param name="predicate">lambda with search expression</param>
		/// <returns></returns>
		IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Create new entity
		/// </summary>
		/// <param name="entity"></param>
		void Create(T entity);

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity"></param>
		void Delete(T entity);

		/// <summary>
		/// Edit entity
		/// </summary>
		/// <param name="entity"></param>
		void Edit(T entity);

		/// <summary>
		/// Save entity
		/// </summary>
		void Save();

		/// <summary>
		/// Dispose entities
		/// </summary>
		void Dispose();
	}
}