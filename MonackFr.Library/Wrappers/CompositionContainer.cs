using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace MonackFr.Wrappers
{
	/// <summary>
	/// Wrapper for System.ComponentModel.Composition.Hosting.CompositionContainer
	/// </summary>
	public class CompositionContainer: ICompositionContainer
	{
		private System.ComponentModel.Composition.Hosting.CompositionContainer compositionContainer;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="path"></param>
		public CompositionContainer(string path)
		{
			compositionContainer = new System.ComponentModel.Composition.Hosting.CompositionContainer(
				new System.ComponentModel.Composition.Hosting.AssemblyCatalog(path)
			);
		}

		/// <summary>
		/// Creates composable parts from an array of attributed objects and composes
		/// them in the specified composition container.
		/// </summary>
		/// <param name="parts">An array of attributed objects to compose.</param>
		void ICompositionContainer.ComposeParts(params object[] parts)
		{
			compositionContainer.ComposeParts(parts);
		}
	}
}
