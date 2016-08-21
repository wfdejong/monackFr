namespace MonackFr.Security
{
	/// <summary>
	/// Wrapper to return static Security classes
	/// </summary>
	public class SecurityWrapper
	{
		/// <summary>
		/// return instance of Hash
		/// </summary>
		/// <returns></returns>
		public static IHash Hash()
		{
			return new Hash();
		}
	}
}
