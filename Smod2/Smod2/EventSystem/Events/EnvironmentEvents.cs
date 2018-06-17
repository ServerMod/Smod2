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
        public WarheadStartEvent(Player activator, float timeLeft, bool isResumed): base(activator, timeLeft)
        {
            IsResumed = isResumed;
        }

        public bool IsResumed { get; set; }
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

    public abstract class DecontamitationEvent : Event
    {
        public DecontamitationEvent(float timeLeft)
        {
            this.TimeLeft = timeLeft;
        }

        public float TimeLeft { get; set; }
    }

    public class DecontaminationStartEvent : DecontamitationEvent
    {
        public DecontaminationStartEvent(float timeLeft, bool isActive) : base(timeLeft)
        {
            IsActive = isActive;
        }

        public bool IsActive { get; set; }
        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerDecontaminationStartCountdown)handler).OnStartCountdown(this);
        }
    }

    public class DecontaminationStopEvent : DecontamitationEvent
    {
        public DecontaminationStopEvent(float timeLeft) : base(timeLeft)
        {
        }

        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerDecontaminationStopCountdown)handler).OnStopCountdown(this);
        }
    }

    public class DecontaminationDecontaminateEvent : Event
    {
        public override void ExecuteHandler(IEventHandler handler)
        {
            ((IEventHandlerDecontaminationDecontaminate)handler).OnDecontaminate();
        }
    }
}
