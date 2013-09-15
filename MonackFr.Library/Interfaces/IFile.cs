using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Interfaces
{
	public interface IFile
	{
		bool Exists(string path);
	}
}
