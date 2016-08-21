namespace MonackFr.Wrappers
{
	public interface IRfc2898DeriveBytes
	{
		int IterationCount { set; }
		string Password { set; }
		byte[] Salt { get; set; }

		byte[] GetBytes(int length);
	}
}
