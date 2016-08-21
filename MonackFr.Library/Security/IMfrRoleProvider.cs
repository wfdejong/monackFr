namespace MonackFr.Security
{
	public interface IMfrRoleProvider
	{
		/// <summary>
		/// Creates a monack framework role
		/// </summary>
		/// <param name="mfrRole"></param>
		void CreateMfrRole(IMfrRole mfrRole);
	}
}