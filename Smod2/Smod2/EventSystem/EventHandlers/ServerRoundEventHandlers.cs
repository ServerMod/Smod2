using Smod2.Events;

namespace Smod2.EventHandlers
{
	public interface IEventHandlerRoundStart : IEventHandler
	{
		/// <summary>  
		///  This is the event handler for Round start events (before people are spawned in)
		/// </summary> 
		void OnRoundStart(RoundStartEvent ev);
	}

	public interface IEventHandlerRoundEnd : IEventHandler
	{
		/// <summary>  
		///  This is the event handler for Round end events (when the stats appear on screen)
		/// </summary> 
		void OnRoundEnd(RoundEndEvent ev);
	}

	public interface IEventHandlerConnect : IEventHandler
	{
		/// <summary>  
		///  This is the event handler for connection events, before players have been created, so names and what not are available. See PlayerJoin if you need that information
		/// </summary> 
		void OnConnect(ConnectEvent ev);
	}

	public interface IEventHandlerDisconnect : IEventHandler
	{
		/// <summary>  
		///  This is the event handler for disconnection events.
		/// </summary> 
		void OnDisconnect(DisconnectEvent ev);
	}

	public interface IEventHandlerLateDisconnect : IEventHandler
	{
		/// <summary>  
		///  This is the event handler for disconnection events after the player has disconnected.
		/// </summary> 
		void OnLateDisconnect(LateDisconnectEvent ev);
	}

	public interface IEventHandlerCheckRoundEnd : IEventHandler
	{
		/// <summary>  
		///  This event handler will call everytime the game checks for a round end
		/// </summary> 
		void OnCheckRoundEnd(CheckRoundEndEvent ev);
	}

	public interface IEventHandlerWaitingForPlayers : IEventHandler
	{
		/// <summary>  
		///  This event handler will call when the server is waiting for players
		/// </summary> 
		void OnWaitingForPlayers(WaitingForPlayersEvent ev);
	}

	public interface IEventHandlerRoundRestart : IEventHandler
	{
		/// <summary>  
		///  This event handler will call when the server is about to restart
		/// </summary> 
		void OnRoundRestart(RoundRestartEvent ev);
	}

	public interface IEventHandlerSetServerName : IEventHandler
	{
		/// <summary>  
		///  This event handler will call when the server name is set
		/// </summary> 
		void OnSetServerName(SetServerNameEvent ev);
	}

	public interface IEventHandlerUpdate : IEventHandler
	{
		/// <summary>  
		///  This event handler will call every server tick
		/// </summary> 
		void OnUpdate(UpdateEvent ev);
	}

	public interface IEventHandlerFixedUpdate : IEventHandler
	{
		/// <summary>  
		///  This event handler will call every server tick at a fixed rate (0.02)
		/// </summary> 
		void OnFixedUpdate(FixedUpdateEvent ev);
	}

	public interface IEventHandlerLateUpdate : IEventHandler
	{
		/// <summary>  
		///  This event handler will call after OnUpdate
		/// </summary> 
		void OnLateUpdate(LateUpdateEvent ev);
	}

	public interface IEventHandlerSceneChanged : IEventHandler
	{
		/// <summary>  
		///  This event handler will call when the server scene changes
		/// </summary> 
		void OnSceneChanged(SceneChangedEvent ev);
	}
}

