namespace Smod2.API
{
	public abstract class Scp079Data
	{
		public abstract float Exp { get; set; }
		public abstract int ExpToLevelUp { get; set; }
		public abstract int Level { get; set; }
		public abstract float AP { get; set; }
		public abstract float APPerSecond { get; set; }
		public abstract float MaxAP { get; set; }
		public abstract float SpeakerAPPerSecond { get; set; }
		public abstract float LockedDoorAPPerSecond { get; set; }
		public abstract float Yaw { get; }
		public abstract float Pitch { get; }
		public abstract Room Speaker { get; set; }
		public abstract Vector Camera { get; } //todo: implement api object

		public abstract Door[] GetLockedDoors();
		public abstract void Lock(Door door);
		public abstract void Unlock(Door door);
		public abstract void TriggerTesla(TeslaGate tesla);
		public abstract void Lockdown(Room room);
		public abstract void SetCamera(Vector position, bool lookAt = false);
		public abstract void ShowGainExp(ExperienceType expType);
		public abstract void ShowLevelUp(int level);
		public abstract object GetComponent();
	}
}
