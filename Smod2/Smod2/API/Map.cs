using System.Collections.Generic;
using Smod2.API;

namespace Smod2.API
{
	public abstract class Map
	{
		public abstract List<Item> GetItems(ItemType type, bool world_only);
		public abstract Vector GetRandomSpawnPoint(Role role);
		public abstract List<Vector> GetSpawnPoints(Role role);
		public abstract List<Vector> GetBlastDoorPoints();
		public abstract List<Door> GetDoors();
		public abstract List<PocketDimensionExit> GetPocketDimensionExits();
		public abstract Dictionary<Vector, Vector> GetElevatorTeleportPoints();
		public abstract void Shake();
		public abstract bool WarheadDetonated { get; }
		public abstract bool LCZDecontaminated { get; }
		public abstract void SpawnItem(ItemType type, Vector pos);
	}

	public abstract class Door
	{
		public abstract bool Open { get; set; }
		public abstract bool Destroyed { get; set; }
		public abstract bool DontOpenOnWarhead { get; set; }
		public abstract bool BlockAfterWarheadDetonation { get; set; }
		public abstract bool Locked { get; set; }
		public abstract float LockCooldown { get; set; }
		public abstract Vector Position { get; }
	}

	public enum PocketDimensionExitType
	{
		Killer = 0,
		Exit = 1
	}

	public abstract class PocketDimensionExit
	{
		public abstract PocketDimensionExitType ExitType { get; set; }
		public abstract Vector Position { get; }
	}
}
