using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Wrappers
{
	public interface ICompositionContainer
	{
		string Path { set; }
		void ComposeParts(params object[] parts);
	}
}
