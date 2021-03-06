﻿namespace MonackFr
{
	public class Format
	{
		/// <summary>
		/// Formats a string to css compatible
		/// </summary>
		/// <param name="stringToFormat"></param>
		/// <returns></returns>
		public static string ToCss(string stringToFormat)
		{
			//todo: replace everything not in [a-z][A-Z][0-9] with dash 
			return string.IsNullOrEmpty(stringToFormat) ? stringToFormat : stringToFormat.Replace('.', '-');
		}

		/// <summary>
		/// Formats a string to javascript compatible
		/// </summary>
		/// <param name="stringToFormat"></param>
		/// <returns></returns>
		public static string ToJs(string stringToFormat)
		{
			//todo: replace everything not in [a-z][A-Z][0-9] 
			return stringToFormat.Replace('.', '-').ToLower();
		}
	}
}
