using System;
namespace MonackFr.Security
{
	public interface IMfrRole
	{
		DateTime Creation { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		DateTime LastUpdate { get; set; }
		string Name { get; set; }
	}
}
