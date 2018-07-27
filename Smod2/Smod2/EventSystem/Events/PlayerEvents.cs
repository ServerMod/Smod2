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
		public PlayerSetRoleEvent(Player player, TeamRole teamRole, Role role) : base(player)
		{
			TeamRole = teamRole;
			Role = role;
		}

		public Role Role { get; set; }

		public TeamRole TeamRole { get; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			Player.CallSetRoleEvent = false;
			((IEventHandlerSetRole)handler).OnSetRole(this);
			Player.CallSetRoleEvent = true;
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
		public Door Door { get => door; }
		public bool Allow { get; set; }
		public bool Destroy { get; set; }

		private Door door;

		public PlayerDoorAccessEvent(Player player, Door door) : base(player)
		{
			this.door = door;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerDoorAccess)handler).OnDoorAccess(this);
		}
	}

	public class PlayerIntercomEvent : PlayerEvent
	{
		public float SpeechTime { get; set; }
		public float CooldownTime { get; set; }
		public bool AllowSpeech { get; set; }

		public PlayerIntercomEvent(Player player, float speechTime, float cooldownTime, bool allowSpeech) : base(player)
		{
			SpeechTime = speechTime;
			CooldownTime = cooldownTime;
			AllowSpeech = allowSpeech;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerIntercom)handler).OnIntercom(this);
		}
	}

	public class PlayerIntercomCooldownCheckEvent : PlayerEvent
	{
		public float CurrentCooldown { get; set; }

		public PlayerIntercomCooldownCheckEvent(Player player, float currCooldownTime) : base(player)
		{
			CurrentCooldown = currCooldownTime;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerIntercomCooldownCheck)handler).OnIntercomCooldownCheck(this);
		}
	}

	public class PlayerPocketDimensionExitEvent : PlayerEvent
	{
		public Vector ExitPosition { get; set; }

		public PlayerPocketDimensionExitEvent(Player player, Vector exitPosition) : base(player)
		{
			ExitPosition = exitPosition;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPocketDimensionExit)handler).OnPocketDimensionExit(this);
		}
	}

	public class PlayerPocketDimensionEnterEvent : PlayerEvent
	{
		public float Damage { get; set; }
		public Vector LastPosition { get; }
		public Vector TargetPosition { get; set; }

		public PlayerPocketDimensionEnterEvent(Player player, float damage, Vector lastPosition, Vector targetPosition) : base(player)
		{
			Damage = damage;
			LastPosition = lastPosition;
			TargetPosition = targetPosition;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPocketDimensionEnter)handler).OnPocketDimensionEnter(this);
		}
	}

	public class PlayerPocketDimensionDieEvent : PlayerEvent
	{
		public bool Die { get; set; }

		public PlayerPocketDimensionDieEvent(Player player, bool die) : base(player)
		{
			Die = die;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPocketDimensionDie)handler).OnPocketDimensionDie(this);
		}
	}

	public class PlayerThrowGrenadeEvent : PlayerEvent
	{
		public ItemType GrenadeType { get; }
		public Vector Direction { get; }
		public bool SlowThrow { get; }

		public PlayerThrowGrenadeEvent(Player player, ItemType grenadeType, Vector direction, bool slowThrow) : base(player)
		{
			this.GrenadeType = grenadeType;
			this.Direction = direction;
			this.SlowThrow = slowThrow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerThrowGrenade)handler).OnThrowGrenade(this);
		}
	}

	public class PlayerInfectedEvent : PlayerEvent
	{
		public float Damage { get; set; }
		public Player Attacker { get; }
		public float InfectTime { get; set; }

		public PlayerInfectedEvent(Player player, float damage, Player attacker, float infectTime) : base(player)
		{
			this.Damage = damage;
			this.Attacker = attacker;
			this.InfectTime = infectTime;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerInfected)handler).OnPlayerInfected(this);
		}
	}

	public class PlayerSpawnRagdollEvent : PlayerEvent
	{
		public Role Role { get; set; }
		public Vector Position { get; set; }
		public Vector Rotation { get; set; }
		public Player Attacker { get; }
		public DamageType DamageType { get; set; }
		public bool AllowRecall { get; set; }

		public PlayerSpawnRagdollEvent(Player player, Role role, Vector position, Vector rotation, Player attacker, DamageType damageType, bool allowRecall) : base(player)
		{
			this.Role = role;
			this.Position = position;
			this.Rotation = rotation;
			this.Attacker = attacker;
			this.DamageType = damageType;
			this.AllowRecall = allowRecall;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSpawnRagdoll)handler).OnSpawnRagdoll(this);
		}
	}

	public class PlayerLureEvent : PlayerEvent
	{
		public bool AllowContain { get; set; }

		public PlayerLureEvent(Player player, bool allowContain) : base(player)
		{
			this.AllowContain = allowContain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerLure)handler).OnLure(this);
		}
	}

	public class PlayerContain106Event : PlayerEvent
	{
		public Player[] SCP106s { get; }

		public PlayerContain106Event(Player player, Player[] scp106s) : base(player)
		{
			this.SCP106s = scp106s;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerContain106)handler).OnContain106(this);
		}
	}
}
