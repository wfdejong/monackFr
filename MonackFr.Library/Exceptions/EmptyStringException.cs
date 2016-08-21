using System;
using System.Runtime.Serialization;

namespace MonackFr
{
	/// <summary>
	/// Thrown if string is empty
	/// </summary>
	[Serializable]
	public class EmptyStringException : Exception
	{
		public EmptyStringException()
			: base() { }

		public EmptyStringException(string message)
			: base(message) { }

		public EmptyStringException(string format, params object[] args)
			: base(string.Format(format, args)) { }

		public EmptyStringException(string message, Exception innerException)
			: base(message, innerException) { }

		public EmptyStringException(string format, Exception innerException, params object[] args)
			: base(string.Format(format, args), innerException) { }

		protected EmptyStringException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}
}
