using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr
{
	public interface ILoader<T>
	{
		IEnumerable<T> LoadedItems { get; }

		ILoader<T> Load(string path);
	}
}
