using MonackFr.Library.Repository;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		bool Authenticate(string userName, string password);
		string[] GetRoles(string userName);
		void AddGroupsToUser(User user, params Group[] groups);
		void RemoveAllGroupsFromUser(User user);
	}
}