namespace Smod2.API
{
	public abstract class UserGroup
	{
		public abstract string Text { get; }
		public abstract string Color { get; }
		public abstract ulong Permissions { get; }
		public abstract string Name { get; }
	}
}
