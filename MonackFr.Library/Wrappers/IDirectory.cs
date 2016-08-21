namespace MonackFr.Wrappers
{
	public interface IDirectory
	{
		string[] GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption);
	}
}
