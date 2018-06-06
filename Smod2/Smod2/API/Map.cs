using System.Collections.Generic;


namespace Smod2.API
{
	public abstract class Map
	{
		public abstract List<Item> GetItems(ItemType type, bool world_only); // may be removed.
		public abstract Vector GetRandomSpawnPoint(Role role);
		public abstract List<Vector> GetSpawnPoints(Role role);
		public abstract Dictionary<Vector, Vector> GetElevatorTeleportPoints();
	}
}
