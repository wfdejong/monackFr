using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MonackFr.Module
{
	/// <summary>
	/// Interface for dbcontext. Used for installing the database table for the context.
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