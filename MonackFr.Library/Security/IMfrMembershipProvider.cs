namespace MonackFr.Security
{
	public interface IMfrMembershipProvider
	{
		/// <summary>
		/// returns monack framework  user
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		MfrUser GetMfrUser(string userName);
	}
}
