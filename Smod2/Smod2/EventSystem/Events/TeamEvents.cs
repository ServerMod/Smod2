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
}
