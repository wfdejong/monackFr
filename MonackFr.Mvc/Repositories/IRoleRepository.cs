using MonackFr.Repository;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Repositories
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		void AddUsersToRoles(string[] userNames, string[] roleNames);
		void RemoveUsersFromRoles(string[] userNames, string[] roleNames);
	}
}