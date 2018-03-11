
using Smod2.API;

namespace Smod2.Events
{
	public interface IEventRoundStart : IEvent
	{
		// Called when the round starts (as soon as players can join)
		void OnRoundStart(Server server);
	}

	public interface IEventRoundEnd : IEvent
	{
		// called at round end
		void OnRoundEnd(Server server, Round round);
	}

	public interface IEventUpdate : IEvent
	{
		// called every frame
		void OnUpdate();
	}

	public interface IEventConnect : IEvent
	{
		void OnConnect(Connection conn);
	}

	public interface IEventDisconnect : IEvent
	{
		void OnDisconnect(Connection conn);
	}
}

