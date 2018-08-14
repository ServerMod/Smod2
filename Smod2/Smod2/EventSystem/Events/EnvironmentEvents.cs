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

	public abstract class WarheadEvent : Event
	{
        public WarheadEvent(Player player, float timeLeft)
        {
            this.player = player;
            this.TimeLeft = timeLeft;
            this.Cancel = false;
        }

		public float TimeLeft { get; set; }
        private Player player;
        public Player Activator { get => player; }
        public bool Cancel { get; set; }
	}

	public class WarheadStartEvent : WarheadEvent
	{
        public WarheadStartEvent(Player activator, float timeLeft, bool isResumed, bool openDoorsAfter): base(activator, timeLeft)
        {
            IsResumed = isResumed;
			OpenDoorsAfter = openDoorsAfter;

		}

        public bool IsResumed { get; set; }
		public bool OpenDoorsAfter { get; set; }
		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerWarheadStartCountdown)handler).OnStartCountdown(this);
		}
	}

	public class WarheadStopEvent : WarheadEvent
	{
        public WarheadStopEvent(Player player, float timeLeft) : base(player, timeLeft)
        {
        }

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

	public class LCZDecontaminateEvent : Event
	{
		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerLCZDecontaminate)handler).OnDecontaminate();
		}
	}

}
