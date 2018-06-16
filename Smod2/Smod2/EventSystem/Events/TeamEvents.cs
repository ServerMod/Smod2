using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace Smod2.EventSystem.Events
{
	public class DecideRespawnQueueEvent : Event
	{
		public Team[] Teams { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerDecideTeamRespawnQueue)handler).OnDecideTeamRespawnQueue(this);
		}
	}

	public class TeamRespawnEvent : Event
	{
		public TeamRespawnEvent(List<Player> playerlist, bool isCI)
		{
			PlayerList = playerlist;
			SpawnChaos = isCI;
		}

		public List<Player> PlayerList { get; }

		public bool SpawnChaos { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerTeamRespawn)handler).OnTeamRespawn(this);
		}
	}

	public class SetRoleMaxHPEvent : Event
	{
		public SetRoleMaxHPEvent(Role role, int maxHP)
		{
			Role = role;
			MaxHP = maxHP;
		}

		public Role Role { get; }
		
		public int MaxHP { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSetRoleMaxHP)handler).OnSetRoleMaxHP(this);
		}
	}

	public class SetSCPAmountEvent : Event
	{
		public SetSCPAmountEvent(int sm049Amount, int sm079Amount, int sm096Amount, int sm106Amount, int sm173Amount, int sm457Amount)
		{
			SCP049amount = sm049Amount;
			SCP079amount = sm079Amount;
			SCP096amount = sm096Amount;
			SCP106amount = sm106Amount;
			SCP173amount = sm106Amount;
			SCP457amount = sm457Amount;
		}

		public int SCP049amount { get; set; }

		public int SCP079amount { get; set; }

		public int SCP096amount { get; set; }

		public int SCP106amount { get; set; }

		public int SCP173amount { get; set; }

		public int SCP457amount { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSetSCPAmount)handler).OnSetSCPAmount(this);
		}
	}
}
