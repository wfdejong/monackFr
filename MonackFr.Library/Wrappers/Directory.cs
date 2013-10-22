using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MonackFr.Wrappers
{
	public class Directory : IDirectory
	{
		string[] IDirectory.GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return System.IO.Directory.GetFiles(path, searchPattern, searchOption);
		}
	}
}
