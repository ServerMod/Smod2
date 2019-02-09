using Smod2.API;
using Smod2.EventHandlers;

namespace Smod2.Events
{
	public abstract class ServerEvent : Event
	{
		private Server server;
		public Server Server { get => server; }

		public ServerEvent(Server server)
		{
			this.server = server;
		}
	}

	public class RoundStartEvent : ServerEvent
	{
		public RoundStartEvent(Server server) : base(server)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerRoundStart)handler).OnRoundStart(this);
		}
	}

	public class RoundEndEvent : ServerEvent
	{
		public RoundEndEvent(Server server, Round round, ROUND_END_STATUS status) : base(server)
		{
			this.round = round;
			this.status = status;
		}

		private Round round;
		public Round Round { get => round; }
		private ROUND_END_STATUS status;
		public ROUND_END_STATUS Status { get => status; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerRoundEnd)handler).OnRoundEnd(this);
		}
	}

	public abstract class ConnectionEvent : Event
	{
		private Connection connection;
		public Connection Connection { get => connection; }

		public ConnectionEvent(Connection connection)
		{
			this.connection = connection;
		}
	}

	public class ConnectEvent : ConnectionEvent
	{
		public ConnectEvent(Connection connection) : base(connection)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerConnect)handler).OnConnect(this);
		}
	}

	public class DisconnectEvent : ConnectionEvent
	{
		public DisconnectEvent(Connection connection) : base(connection)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerDisconnect)handler).OnDisconnect(this);
		}
	}

	public class LateDisconnectEvent : ConnectionEvent
	{
		public LateDisconnectEvent(Connection connection) : base(connection)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerLateDisconnect)handler).OnLateDisconnect(this);
		}
	}

	public class CheckRoundEndEvent : ServerEvent
	{
		public CheckRoundEndEvent(Server server, Round round) : base(server)
		{
			this.round = round;
		}

		private Round round;
		public Round Round { get => round; }
		public ROUND_END_STATUS Status { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerCheckRoundEnd)handler).OnCheckRoundEnd(this);
		}
	}

	public class WaitingForPlayersEvent : ServerEvent
	{
		public WaitingForPlayersEvent(Server server) : base(server)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerWaitingForPlayers)handler).OnWaitingForPlayers(this);
		}
	}

	public class RoundRestartEvent : ServerEvent
	{
		public RoundRestartEvent(Server server) : base(server)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerRoundRestart)handler).OnRoundRestart(this);
		}
	}

	public class SetServerNameEvent : ServerEvent
	{
		public SetServerNameEvent(Server server, string ServerName) : base(server)
		{
			this.ServerName = ServerName;
		}

		public string ServerName;

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSetServerName)handler).OnSetServerName(this);
		}
	}

	public class UpdateEvent : Event
	{
		public UpdateEvent()
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerUpdate)handler).OnUpdate(this);
		}
	}

	public class FixedUpdateEvent : Event
	{
		public FixedUpdateEvent()
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerFixedUpdate)handler).OnFixedUpdate(this);
		}
	}

	public class LateUpdateEvent : Event
	{
		public LateUpdateEvent()
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerLateUpdate)handler).OnLateUpdate(this);
		}
	}

	public class SceneChangedEvent : Event
	{
		public string SceneName { get; }

		public SceneChangedEvent(string sceneName)
		{
			this.SceneName = sceneName;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSceneChanged)handler).OnSceneChanged(this);
		}
	}
}
