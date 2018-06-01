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
}

