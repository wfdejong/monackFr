namespace MonackFr.Library.Module
{
	/// <summary>
	/// Definition of a module
	/// </summary>
	public interface IModule
	{
		/// <summary>
		/// The name of the module
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The unique name within the system
		/// </summary>
		string SystemName { get; }

		/// <summary>
		/// Description of the module
		/// </summary>
		string Description { get; }

		/// <summary>
		/// The author of the module
		/// </summary>
		string Author { get; }

		/// <summary>
		/// Returns module's tile
		/// </summary>
		/// <returns></returns>
		Tile GetTile();
	}
}