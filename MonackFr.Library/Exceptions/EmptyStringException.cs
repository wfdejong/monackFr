using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr
{
	public class EmptyStringException : Exception
	{
		public override string Message
		{
			get
			{
				return "String is empty";
			}
		}
	}
}
