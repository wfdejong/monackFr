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
	public class TestRoleRepository : IRoleRepository
	{
		/// <summary>
		/// entries used used for testing
		/// </summary>
		private List<Role> _entities;

		/// <summary>
		/// Indicates id the save method is hit.
		/// </summary>
		public Boolean SaveMethodHit { get; set; }

		/// <summary>
		/// constructor
		/// </summary>
		public TestRoleRepository()
		{
			_entities = new List<Role>();
			_entities.Add(new Role
			{
				Id = 1,
				Creation = DateTime.Now,
				LastUpdate = DateTime.Now,
				Name = "TestRole",
				Description = "Test descrition"
			});
		}

		/// <summary>
		/// returns all _entries
		/// </summary>
		/// <returns></returns>
		public IQueryable<Role> GetAll()
		{
			return _entities.AsQueryable<Role>();
		}

		public Role GetSingle(Expression<Func<Role, bool>> predicate)
		{
			IQueryable<Role> roles = _entities.AsQueryable<Role>();
			return roles.Where(predicate).FirstOrDefault<Role>();	
		}

		public IQueryable<Role> FindBy(Expression<Func<Role, bool>> predicate)
		{
			IQueryable<Role> roles = _entities.AsQueryable<Role>();
			return roles.Where(predicate);			
		}

		public void Create(Role role)
		{
			_entities.Add(role);
		}

		public void Delete(Role role)
		{
			_entities.Remove(role);
		}

		public void Edit(Role role)
		{
			Role entity = _entities.Find(r => r.Id==role.Id);
			Int32 index = _entities.IndexOf(entity);
			_entities[index] = role;
		}

		public void Save()
		{
			SaveMethodHit = true;
		}

		public void AddUsersToRoles(String[] userNames, String[] roleNames)
		{
			throw new NotImplementedException();
		}

		public void RemoveUsersFromRoles(String[] userNames, String[] roleNames)
		{
			throw new NotImplementedException();
		}
	}
}
