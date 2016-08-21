namespace MonackFr.Mvc.ViewModels
{
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
        /// Angular controller
        /// </summary>
        public string Controller { get; set; }
    }
}