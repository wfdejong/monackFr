using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonackFr.Mvc.Areas.UserManagement.Models;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web.Security;

namespace MonackFr.Mvc.Areas.UserManagement.Tests
{
	public class TestUserRepository : IUserRepository
	{
		/// <summary>
		/// entries used used for testing
		/// </summary>
		private List<User> _entities;

		/// <summary>
		/// Indicates id the save method is hit.
		/// </summary>
		public Boolean SaveMethodHit { get; set; }

		/// <summary>
		/// constructor
		/// </summary>
		public TestUserRepository()
		{
			_entities = new List<User>();
			_entities.Add(new User
			{
				Id = 1,
				//CreationDate = DateTime.Now,
				LastUpdate = DateTime.Now,
				//Username = "TestUserName"
			});
		}

		/// <summary>
		/// returns all _entries
		/// </summary>
		/// <returns></returns>
		public IQueryable<User> GetAll()
		{
			return _entities.AsQueryable<User>();
		}

		public User GetSingle(Expression<Func<User, bool>> predicate)
		{
			IQueryable<User> users = _entities.AsQueryable<User>();
			return users.Where(predicate).FirstOrDefault<User>();	
		}

		public IQueryable<User> FindBy(Expression<Func<User, bool>> predicate)
		{
			IQueryable<User> users = _entities.AsQueryable<User>();
			return users.Where(predicate);			
		}

		public void Create(User user)
		{
			_entities.Add(user);
		}

		public void Delete(User user)
		{
			_entities.Remove(user);
		}

		public void Edit(User user)
		{
			User entity = _entities.Find(u => u.Id==user.Id);
			Int32 index = _entities.IndexOf(entity);
			_entities[index] = user;
		}

		public void Save()
		{
			SaveMethodHit = true;
		}

		public Boolean Authenticate(String userName, String password)
		{
			User user = _entities.FirstOrDefault<User>(u => u.UserName == userName && u.Password == password);
			return (user != null);
		}

		public String[] GetRoles(String userName)
		{
			throw new NotImplementedException();
		}
	}
}
