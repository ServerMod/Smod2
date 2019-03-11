namespace Smod2.API
{
	public abstract class UserGroup
	{
		public abstract string Color { get; }
		public abstract string BadgeText { get; }
		public abstract ulong Permissions { get; }
		public abstract bool Cover { get; }
		public abstract bool HiddenByDefault { get; }
		public abstract string Name { get; }
		public abstract object GetComponent();
	}
}
