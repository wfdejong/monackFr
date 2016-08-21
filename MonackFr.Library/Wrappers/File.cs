namespace MonackFr.Wrappers
{
	public class File : IFile
	{
		bool IFile.Exists(string path)
		{
			return System.IO.File.Exists(path);
		}
	}
}
