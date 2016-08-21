using System.Collections.Generic;

namespace MonackFr
{
	public interface ILoader<T>
	{
		IEnumerable<T> LoadedItems { get; }

		ILoader<T> Load(string path);
	}
}
