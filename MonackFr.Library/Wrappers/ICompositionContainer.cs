using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Wrappers
{
	public interface ICompositionContainer
	{
		void ComposeParts(params object[] parts);
	}
}
