using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace Smod2.EventSystem.Events
{
	public class DecideRespawnQueueEvent : Event
	{
		public DecideRespawnQueueEvent(Team[] teams)
		{
			Teams = teams;
		}

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

		public List<Player> PlayerList { get; set; }

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

	public class SetSCPConfigEvent : Event
	{
		public SetSCPConfigEvent(bool ban049, bool ban079, bool ban096, bool ban106, bool ban173, bool ban939_53, bool ban939_89, int sm049Amount, int sm079Amount, int sm096Amount, int sm106Amount, int sm173Amount, int sm939_53Amount, int sm939_89Amount)
		{
			Ban049 = ban049;
			Ban079 = ban079;
			Ban096 = ban096;
			Ban106 = ban106;
			Ban173 = ban173;
			Ban939_53 = ban939_53;
			Ban939_89 = ban939_89;

			SCP049amount = sm049Amount;
			SCP079amount = sm079Amount;
			SCP096amount = sm096Amount;
			SCP106amount = sm106Amount;
			SCP173amount = sm106Amount;
			SCP939_53amount = sm939_53Amount;
			SCP939_89amount = sm939_89Amount;
		}

		public bool Ban049 { get; set; }

		public bool Ban079 { get; set; }

		public bool Ban096 { get; set; }

		public bool Ban106 { get; set; }

		public bool Ban173 { get; set; }

		public bool Ban939_53 { get; set; }

		public bool Ban939_89 { get; set; }

		public int SCP049amount { get; set; }

		public int SCP079amount { get; set; }

		public int SCP096amount { get; set; }

		public int SCP106amount { get; set; }

		public int SCP173amount { get; set; }

		public int SCP457amount { get; set; }

		public int SCP939_53amount { get; set; }

		public int SCP939_89amount { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSetSCPConfig)handler).OnSetSCPConfig(this);
		}
	}

	public class SetNTFUnitNameEvent : Event
	{
		public SetNTFUnitNameEvent(string unit)
		{
			this.Unit = unit;
		}

		public string Unit { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSetNTFUnitName)handler).OnSetNTFUnitName(this);
		}
	}
}
