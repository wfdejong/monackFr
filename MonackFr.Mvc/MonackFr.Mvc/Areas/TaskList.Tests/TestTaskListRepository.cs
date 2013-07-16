using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonackFr.Mvc.Areas.TaskList.Models;
using System.Linq.Expressions;

namespace MonackFr.Mvc.Areas.TaskList.Tests
{
	public class TestTaskListRepository : ITaskListRepository
	{
		/// <summary>
		/// entries used used for testing
		/// </summary>
		private IQueryable<Task> _entries;

		/// <summary>
		/// constructor
		/// </summary>
		public TestTaskListRepository()
		{
			List<Task> tasks = new List<Task>();

			tasks.Add(new Task
			{
				Id = 1,
				InsertDate = DateTime.Now,
				LastUpdate = DateTime.Now
			});

			_entries = tasks.AsQueryable<Task>();
		}

		/// <summary>
		/// returns all _entries
		/// </summary>
		/// <returns></returns>
		public IQueryable<Task> GetAll()
		{
			return _entries;
		}

		public Task GetSingle(Expression<Func<Task, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Task> FindBy(Expression<Func<Task, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public void Create(Task task)
		{
			throw new NotImplementedException();
		}

		public void Delete(Task task)
		{
			throw new NotImplementedException();
		}

		public void Edit(Task task)
		{
			throw new NotImplementedException();
		}

		public void Save()
		{
			throw new NotImplementedException();
		}
	}
}
