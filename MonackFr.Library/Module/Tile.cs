namespace MonackFr.Library.Module
{
	/// <summary>
	/// Tile definition for in a module
	/// </summary>
	public class Tile
	{
        private IModule _module;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="module">the system name of the module</param>
        public Tile(IModule module)
        {
            _module = module;
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Items shown in tile
        /// </summary>
        public string[] PreviewItems { get; set; }

        /// <summary>
        /// Copyright text
        /// </summary>
        public string Copyright { get; set; }
    }
}