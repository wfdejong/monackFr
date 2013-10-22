using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Wrappers
{
	public interface IDirectory
	{
		string[] GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption);
	}
}
