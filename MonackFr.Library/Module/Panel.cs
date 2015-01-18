using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Repository
{
	public class Panel
	{
		private string _systemName;

		public string SystemName
		{
			get
			{
				return _systemName;
			}
		}
		public string Url { get; set; }
		public string OnShow { get; set; }
		public string CssSystemName
		{
			get
			{
				return Format.ToCss(_systemName);
			}
		}

		public Panel(string systemName)
		{
			_systemName = systemName;
		}
	}
}
