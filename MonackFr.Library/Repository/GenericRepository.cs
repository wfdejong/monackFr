using System;
using System.Linq;
using System.Data.Entity;

namespace MonackFr.Library.Repository
{
	/// <summary>
	/// Generic repository
	/// </summary>
	/// <typeparam name="C"></typeparam>
	/// <typeparam name="T"></typeparam>
	public abstract class GenericRepository<C, T> :
	IGenericRepository<T>, IDisposable
		where T : class
		where C : DbContext, new()
	{
		/// <summary>
		/// db context entities
		/// </summary>
		protected C _entities = new C();
		
		/// <summary>
		/// Db context entities
		/// </summary>
		public C Entities
		{
			get { return _entities; }
			set { _entities = value; }
		}

	    public GenericRepository()
	    {
	        _entities.Configuration.LazyLoadingEnabled = false;
	    }

		/// <summary>
		/// Get all entities
		/// </summary>
		/// <returns></returns>
		public virtual IQueryable<T> GetAll()
		{
			IQueryable<T> query = _entities.Set<T>();
			return query;
		}

		/// <summary>
		/// Get entity
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{			
			return _entities.Set<T>().Where(predicate).FirstOrDefault<T>();
		}

		/// <summary>
		/// Find entity
		/// </summary>
		/// <param name="predicate">lambda with search expression</param>
		/// <returns></returns>
		public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{
			IQueryable<T> query = _entities.Set<T>().Where(predicate);
			return query;
		}

		/// <summary>
		/// Create new entity
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Create(T entity)
		{
			_entities.Set<T>().Add(entity);
		}

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Delete(T entity)
		{
			_entities.Set<T>().Remove(entity);
		}

		/// <summary>
		/// Edit entity
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Edit(T entity)
		{
			_entities.Entry(entity).State = EntityState.Modified;
		}

		/// <summary>
		/// Save changes db context
		/// </summary>
		public virtual void Save()
		{
			_entities.SaveChanges();			
		}

		/// <summary>
		/// Dispose entities
		/// </summary>
		public void Dispose()
		{
			_entities.Dispose();
		}

		/// <summary>
		/// Desctructor
		/// </summary>
		~GenericRepository()
		{
			Dispose();
		}
	}
}