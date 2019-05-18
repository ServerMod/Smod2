using Smod2.Events;

namespace Smod2.EventHandlers
{
	public interface IEventHandlerSCP914Activate : IEventHandler
	{
		/// <summary>  
		///  This is the event handler for when a SCP914 is activated
		/// </summary> 
		void OnSCP914Activate(SCP914ActivateEvent ev);
	}

	public interface IEventHandlerWarheadStartCountdown : IEventHandler //Before the countdown is started
	{
		/// <summary>  
		///  This is the event handler for when the warhead starts counting down, isResumed is false if its the initial count down. Note: activator can be null
		/// </summary> 
		void OnStartCountdown(WarheadStartEvent ev);
	}
	public interface IEventHandlerWarheadStopCountdown : IEventHandler //Before the countdown is stopped
	{
		/// <summary>  
		///  This is the event handler for when the warhead stops counting down.
		/// </summary> 
		void OnStopCountdown(WarheadStopEvent ev);
	}

	public interface IEventHandlerWarheadChangeLever : IEventHandler
	{
		void OnChangeLever(WarheadChangeLeverEvent ev);
	}

	public interface IEventHandlerWarheadKeycardAccess : IEventHandler
	{
		void OnWarheadKeycardAccess(WarheadKeycardAccessEvent ev);
	}
	
	public interface IEventHandlerWarheadDetonate : IEventHandler
	{
		/// <summary>  
		///  This is the event handler for when the warhead is about to detonate (so before it actually triggers)
		/// </summary> 
		void OnDetonate();
	}

	public interface IEventHandlerLCZDecontaminate : IEventHandler
	{ 
		/// <summary>  
		///  This is the event handler for when the LCZ is decontaminated
		/// </summary> 
		void OnDecontaminate();
	}

	public interface IEventHandlerSummonVehicle : IEventHandler
	{
		/// <summary>  
		/// Called when a van/chopper is summoned.
		/// </summary>  
		void OnSummonVehicle(SummonVehicleEvent ev);
	}

	public interface IEventHandlerGeneratorFinish : IEventHandler
	{
		/// <summary>
		/// Called when a generator becomes engaged.
		/// </summary>
		void OnGeneratorFinish(GeneratorFinishEvent ev);
	}
	
	public interface IEventHandlerScpDeathAnnouncement : IEventHandler
	{
		/// <summary>
		/// Called when a C.A.S.S.I.E. announcement gets added for an SCP death.
		/// </summary>
		void OnScpDeathAnnouncement(ScpDeathAnnouncementEvent ev);
	}
}
