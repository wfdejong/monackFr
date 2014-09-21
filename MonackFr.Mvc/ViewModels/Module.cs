using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.ViewModels
{
	public class Module
	{
		/// <summary>
		/// The module name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Description of the module
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The author of the module
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// The system name used internally. Should be unique in the whole application
		/// </summary>
		public string SystemName {get;set; }

		/// <summary>
		/// The system name used in Css
		/// </summary>
		public string CssSystemName
		{
			get
			{
				return Format.ToCss(SystemName);
			}
		}
	}
}