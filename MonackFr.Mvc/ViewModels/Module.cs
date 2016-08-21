namespace MonackFr.Mvc.ViewModels
{
	public class Module
	{
        public int Id { get; set; }

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
		public string JsSystemName
		{
			get
			{
				return Format.ToJs(SystemName);
			}
		}
	}
}