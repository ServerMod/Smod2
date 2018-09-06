namespace Smod2.API
{
	public abstract class Connection
	{
		public abstract string IpAddress { get; }
		public abstract void Disconnect();
		public abstract bool IsBanned { get; }
	}
}
