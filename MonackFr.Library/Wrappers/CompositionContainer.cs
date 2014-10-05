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
		/// <summary>
		/// Path to the assembly
		/// </summary>
		private string _path;

		/// <summary>
		/// Path to the assembly
		/// </summary>
		string ICompositionContainer.Path 
		{	
			set
			{
				_path = value;
			}
		}
		
		/// <summary>
		/// Creates composable parts from an array of attributed objects and composes
		/// them in the specified composition container.
		/// </summary>
		/// <param name="parts">An array of attributed objects to compose.</param>
		void ICompositionContainer.ComposeParts(params object[] parts)
		{
			System.ComponentModel.Composition.Hosting.CompositionContainer compositionContainer = new System.ComponentModel.Composition.Hosting.CompositionContainer(
				new System.ComponentModel.Composition.Hosting.AssemblyCatalog(_path)
			);
			compositionContainer.ComposeParts(parts);
		}
	}
}
