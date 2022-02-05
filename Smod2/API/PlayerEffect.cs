namespace Smod2.API
{
	public abstract class PlayerEffect
	{
		public abstract StatusEffect StatusEffect { get; }
		public abstract float Duration { get; set; }
		public abstract float Intensity { get; set; }
		public abstract void Enable(float duration, bool AddToCurrentDuration = false);
		public abstract void Disable();
	}
}
