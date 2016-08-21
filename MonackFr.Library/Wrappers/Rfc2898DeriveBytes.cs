namespace MonackFr.Wrappers
{
	public class Rfc2898DeriveBytes : IRfc2898DeriveBytes	
	{
		System.Security.Cryptography.Rfc2898DeriveBytes _deriveBytes;
		int _iterationCount;
		string _password;
		byte[] _salt;
		
		int IRfc2898DeriveBytes.IterationCount
		{
			set
			{
				_iterationCount = value;
			}
		}

		string IRfc2898DeriveBytes.Password
		{
			set
			{
				_password = value;
			}
		}

		byte[] IRfc2898DeriveBytes.Salt
		{
			get
			{
				return _salt;
			}
			set
			{
				_salt = value;
			}
		}

		byte[] IRfc2898DeriveBytes.GetBytes(int length)
		{
			_deriveBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(_password, _salt);
			_deriveBytes.IterationCount = _iterationCount;
			return _deriveBytes.GetBytes(length);
		}
	}
}
