namespace MonackFr.Wrappers
{
	public interface ICompositionContainer
	{
		string Path { set; }
		void ComposeParts(params object[] parts);
	}
}
