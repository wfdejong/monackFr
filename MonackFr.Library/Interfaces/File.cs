using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonackFr.Interfaces
{
	public class File : IFile
	{
		bool IFile.Exists(string path)
		{
			return System.IO.File.Exists(path);
		}
	}
}
