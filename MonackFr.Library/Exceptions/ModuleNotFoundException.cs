using System;
using System.Runtime.Serialization;

namespace MonackFr
{
	[Serializable]
	public class ModuleNotFoundException : Exception
	{
		public ModuleNotFoundException()
			: base() { }

		public ModuleNotFoundException(string message)
			: base(message) { }

		public ModuleNotFoundException(string format, params object[] args)
			: base(string.Format(format, args)) { }

		public ModuleNotFoundException(string message, Exception innerException)
			: base(message, innerException) { }

		public ModuleNotFoundException(string format, Exception innerException, params object[] args)
			: base(string.Format(format, args), innerException) { }

		protected ModuleNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}
}
