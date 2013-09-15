using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace MonackFr.Interfaces
{
	public class CompositionContainer: ICompositionContainer
	{
		private System.ComponentModel.Composition.Hosting.CompositionContainer compositionContainer;

		public CompositionContainer(string path)
		{
			compositionContainer = new System.ComponentModel.Composition.Hosting.CompositionContainer(
				new System.ComponentModel.Composition.Hosting.AssemblyCatalog(path)
			);
		}

		void ICompositionContainer.ComposeParts(params object[] parts)
		{
			compositionContainer.ComposeParts(parts);
		}
	}
}
