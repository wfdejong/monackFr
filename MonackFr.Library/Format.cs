using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr
{
	public class Format
	{
		/// <summary>
		/// Formats a string to css compatible test
		/// </summary>
		/// <param name="stringToFormat"></param>
		/// <returns></returns>
		public static string ToCss(string stringToFormat)
		{
			//todo: replace everything not in [a-z][A-Z][0-9] with 
			return String.IsNullOrEmpty(stringToFormat) ? stringToFormat : stringToFormat.Replace('.', '-');
		}
	}
}
