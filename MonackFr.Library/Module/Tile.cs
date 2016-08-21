namespace MonackFr.Library.Module
{
	/// <summary>
	/// Tile definition for in a module
	/// </summary>
	public class Tile
	{
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

        /// <summary>
        /// Name of angular controll called after clicking tile
        /// </summary>
        public string Controller { get; set; }
    }
}