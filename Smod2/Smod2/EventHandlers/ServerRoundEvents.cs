
using Smod2.API;

namespace Smod2.Events
{
	public interface IEventRoundStart : IEventHandler
	{
		// Called when the round starts (as soon as players can join)
		void OnRoundStart(Server server);
	}

	public interface IEventRoundEnd : IEventHandler
	{
		// called at round end
		void OnRoundEnd(Server server, Round round);
	}

	public interface IEventUpdate : IEventHandler
	{
		// called every frame
		void OnUpdate();
	}

	public interface IEventConnect : IEventHandler
	{
		void OnConnect(Connection conn);
	}

	public interface IEventDisconnect : IEventHandler
	{
		void OnDisconnect(Connection conn);
	}
}

