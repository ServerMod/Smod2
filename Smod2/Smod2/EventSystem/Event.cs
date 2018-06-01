using Smod2.EventHandlers;

namespace Smod2.Events
{
	public abstract class Event
	{
		public abstract void ExecuteHandler(IEventHandler handler);
	}
}
