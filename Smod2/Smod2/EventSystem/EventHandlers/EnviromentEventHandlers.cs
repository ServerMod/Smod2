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
	public interface IEventHandlerWarheadDetonate : IEventHandler
	{
		/// <summary>  
		///  This is the event handler for when the warhead is about to detonate (so before it actually triggers)
		/// </summary> 
		void OnDetonate();
	}

    public interface IEventHandlerDecontaminationStartCountdown : IEventHandler //Before the countdown is started
    {
        /// <summary>  
        ///  This is the event handler for when the decontamination process starts counting down
        /// </summary> 
        void OnStartCountdown(DecontaminationStartEvent ev);
    }
    public interface IEventHandlerDecontaminationStopCountdown : IEventHandler //Before the countdown is stopped
    {
        /// <summary>  
        ///  This is the event handler for when the decontamination stops counting down.
        /// </summary> 
        void OnStopCountdown(DecontaminationStopEvent ev);
    }
    public interface IEventHandlerDecontaminationDecontaminate : IEventHandler
    {
        /// <summary>  
        ///  This is the event handler for when the decontamination process is about to decontaminate (so before it actually triggers)
        /// </summary> 
        void OnDecontaminate();
    }

}
