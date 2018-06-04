using Smod2.API;
using Smod2.EventHandlers;

namespace Smod2.Events
{
	public abstract class PlayerEvent : Event
	{
		public PlayerEvent(Player player)
		{
			this.player = player;
		}

		private Player player;
		public Player Player { get => player; }
	}

	public class PlayerHurtEvent : PlayerEvent
	{
		public PlayerHurtEvent(Player player, Player attacker, float damage, DamageType damageType) : base(player)
		{
			this.attacker = attacker;
			Damage = damage;
			DamageType = damageType;
		}

		private Player attacker;
		public Player Attacker { get => attacker; }
		public float Damage { get; set; }
		public DamageType DamageType { get; set; }
		
		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerHurt)handler).OnPlayerHurt(this);
		}
	}

	public class PlayerDeathEvent : PlayerEvent
	{
		public PlayerDeathEvent(Player player, Player killer, bool spawnRagdoll): base(player)
		{
			Killer = killer;
			SpawnRagdoll = spawnRagdoll;
		}

		public Player Killer { get; }
		public bool SpawnRagdoll { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerDie)handler).OnPlayerDie(this);
		}
	}

	public abstract class PlayerItemEvent : PlayerEvent
	{
		public PlayerItemEvent(Player player, Item item, ItemType change, bool allow) : base(player)
		{
			Item = item;
			Allow = allow;
            ChangeTo = change;
		}

		public Item Item { get; set; }
        public ItemType ChangeTo { get; set; }
		public bool Allow { get; set; }
	}

	public class PlayerPickupItemEvent : PlayerItemEvent
	{
		public PlayerPickupItemEvent(Player player, Item item, ItemType change, bool allow) : base(player, item, change, allow)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerPickupItem)handler).OnPlayerPickupItem(this);
		}
	}

	public class PlayerDropItemEvent : PlayerItemEvent
	{
		public PlayerDropItemEvent(Player player, Item item, ItemType change, bool allow) : base(player, item, change, allow)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerDropItem)handler).OnPlayerDropItem(this);
		}
	}

	public class PlayerJoinEvent : PlayerEvent
	{
		public PlayerJoinEvent(Player player) : base(player)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerJoin)handler).OnPlayerJoin(this);
		}
	}


	public class PlayerNicknameSetEvent : PlayerEvent
	{
		public PlayerNicknameSetEvent(Player player, string nickname) : base(player)
		{
			Nickname = nickname;
		}

		public string Nickname { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerNicknameSet)handler).OnNicknameSet(this);
		}
	}

	public class PlayerInitialAssignTeamEvent : PlayerEvent
	{
		public PlayerInitialAssignTeamEvent(Player player, Team team) : base(player)
		{
			Team = team;
		}

		public Team Team { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerInitialAssignTeam)handler).OnAssignTeam(this);
		}
	}

	public class PlayerCheckEscapeEvent : PlayerEvent
	{
		public bool AllowEscape { get; set; }
		public Role ChangeRole { get; set; }

		public PlayerCheckEscapeEvent(Player player) : base(player)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerCheckEscape)handler).OnCheckEscape(this);
		}
	}

	public class PlayerSetRoleEvent : PlayerEvent
	{
		public PlayerSetRoleEvent(Player player, TeamRole teamRole) : base(player)
		{
			TeamRole = teamRole;
		}

		public TeamRole TeamRole { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSetRole)handler).OnSetRole(this);
		}
	}

	public class PlayerSpawnEvent : PlayerEvent
	{
		public Vector SpawnPos { get; set; }
		public PlayerSpawnEvent(Player player) : base(player)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSpawn)handler).OnSpawn(this);
		}
	}

	public class PlayerDoorAccessEvent : PlayerEvent
	{
		public bool Destroy { get; set; }
		public bool Allow { get; set; }

		public PlayerDoorAccessEvent(Player player) : base(player)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerDoorAccess)handler).OnDoorAccess(this);
		}
	}

}
