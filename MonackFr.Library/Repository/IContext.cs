﻿using System.Data.Entity;

namespace MonackFr.Library.Repository
{
	/// <summary>
	/// Interface for dbcontext. Contains DbSets for accessing data and structure for database.
	/// </summary>
	public interface IContext
	{
		/// <summary>
		/// Setup database tables
		/// </summary>
		/// <param name="modelBuilder"></param>
		void Setup(DbModelBuilder modelBuilder);
	}
}