using Smod2.Events;

namespace Smod2.EventHandlers
{
	public interface IEventHandlerSetConfig : IEventHandler
	{
		void OnSetConfig(SetConfigEvent ev);
	}
}

