namespace Smod2.API
{
	public abstract class Connection
	{
		public abstract string IpAddres { get; }
		public abstract void Disconnect();
	}
}
