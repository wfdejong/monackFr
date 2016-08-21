namespace MonackFr.Security
{
	public interface IHash
	{
		string Create(string password);
		bool ValidatePassword(string password, string hash);
	}
}
