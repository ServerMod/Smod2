﻿using Smod2.API;
using Smod2.EventHandlers;

namespace Smod2.Events
{
	public class SCP914ActivateEvent : Event
	{
		public Player User { get; }
		public KnobSetting KnobSetting { get; set; }
		public object[] Inputs { get; set; } //TODO: Proper wrapping API
		public Vector IntakePos { get; set; }
		public Vector OutputPos { get; set; }

		public SCP914ActivateEvent(Player user, KnobSetting knobSetting, object[] inputs, Vector intakePos, Vector outputPos)
		{
			this.User = user;
			this.KnobSetting = knobSetting;
			this.Inputs = inputs;
			this.IntakePos = intakePos;
			this.OutputPos = outputPos;
		}

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
	
	// Not really sure why warhead (which have players) is in EnvironmentEvents but ok
	public class WarheadChangeLeverEvent : Event
	{
		public Player Player { get; }
		public bool Allow { get; set; }

		public WarheadChangeLeverEvent(Player player)
		{
			Player = player;
			Allow = true;
		}
		
		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerWarheadChangeLever)handler).OnChangeLever(this);
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

	public class SummonVehicleEvent : Event
	{
		public bool IsCI { get; set; }

		public bool AllowSummon { get; set; }

		public SummonVehicleEvent(bool isCI, bool allowSummon)
		{
			this.IsCI = isCI;
			this.AllowSummon = allowSummon;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSummonVehicle)handler).OnSummonVehicle(this);
		}
	}

	public class GeneratorFinishEvent : Event
	{
		public Generator Generator { get; }

		public GeneratorFinishEvent(Generator generator)
		{
			Generator = generator;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGeneratorFinish)handler).OnGeneratorFinish(this);
		}
	}
}
