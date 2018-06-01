using Smod2.API;
using Smod2.EventHandlers;

namespace Smod2.Events
{
	public class SCP914ActivateEvent : Event
	{
		public KnobSetting KnobSetting { get; set; }
		public object[] Inputs { get; set; } //TODO: Proper wrapping API
		public Vector Intake { get; set; }
		public Vector Outtake { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSCP914Activate)handler).OnSCP914Activate(this);
		}
	}

	public class PokerDimensionExitEvent : Event
	{
		public Vector[] PossibleExists { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPocketDimensionExit)handler).OnPocketDimensionExit(this);
		}
	}

	public abstract class WarheadEvent : Event
	{
		public float TimeLeft { get; set; }
	}

	public class WarheadStartEvent : WarheadEvent
	{
		public bool IsResumed { get; set; }
		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerWarheadStartCountdown)handler).OnStartCountdown(this);
		}
	}

	public class WarheadStopEvent : WarheadEvent
	{
		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerWarheadStopCountdown)handler).OnStopCountdown(this);
		}
	}

	public class WarheadDetonateEvent : Event
	{
		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerWarheadDetonate)handler).OnDetonate();
		}
	}

}
