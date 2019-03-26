using Smod2.API;
using Smod2.EventHandlers;
using System.Collections.Generic;

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
		public PlayerDeathEvent(Player player, Player killer, bool spawnRagdoll, DamageType damageType): base(player)
		{
			Killer = killer;
			SpawnRagdoll = spawnRagdoll;
			DamageTypeVar = damageType;
		}

		public Player Killer { get; }
		public bool SpawnRagdoll { get; set; }
		public DamageType DamageTypeVar { get; set; }

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

	public class PlayerPickupItemLateEvent : PlayerEvent
	{
		public Item Item { get; }

		public PlayerPickupItemLateEvent(Player player, Item item) : base(player)
		{
			this.Item = item;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerPickupItemLate)handler).OnPlayerPickupItemLate(this);
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
		public PlayerSetRoleEvent(Player player, TeamRole teamRole, Role role, List<ItemType> items, bool usingDefaultItem = true) : base(player)
		{
			TeamRole = teamRole;
			Role = role;
			Items = items;
			UsingDefaultItem = usingDefaultItem;
		}

		public List<ItemType> Items { get; set; }

		public bool UsingDefaultItem { get; set; }

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

		public PlayerIntercomEvent(Player player, float speechTime, float cooldownTime) : base(player)
		{
			SpeechTime = speechTime;
			CooldownTime = cooldownTime;
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
		public Player Attacker { get; }

		public PlayerPocketDimensionEnterEvent(Player player, float damage, Vector lastPosition, Vector targetPosition, Player attacker) : base(player)
		{
			Damage = damage;
			LastPosition = lastPosition;
			TargetPosition = targetPosition;
			Attacker = attacker;
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
		public bool ActivateContainment { get; set; }

		public PlayerContain106Event(Player player, Player[] scp106s, bool activateContainment) : base(player)
		{
			this.SCP106s = scp106s;
			this.ActivateContainment = activateContainment;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerContain106)handler).OnContain106(this);
		}
	}

	public class PlayerMedkitUseEvent : PlayerEvent
	{
		public int RecoverHealth { get; set; }

		public PlayerMedkitUseEvent(Player player, int recoverHealth) : base(player)
		{
			this.RecoverHealth = recoverHealth;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerMedkitUse)handler).OnMedkitUse(this);
		}
	}

	public class PlayerShootEvent : PlayerEvent
	{
		public Player Target { get; }
		public DamageType Weapon { get; }
		public bool ShouldSpawnHitmarker { get; set; }
		public bool ShouldSpawnBloodDecal { get; set; }
		public Vector SourcePosition { get; }
		public Vector TargetPosition { get; }
		public string TargetHitbox { get; }
		public WeaponType? WeaponSound { get; set; }
		public Vector Direction { get; set; }

		public PlayerShootEvent(Player player, Player target, DamageType weapon, Vector sourcePosition, Vector targetPosition, string targetHitbox, WeaponType weaponSound, Vector direction, bool spawnHitmarker = true, bool spawnBloodDecal = true) : base(player)
		{
			this.Target = target;
			this.Weapon = weapon;
			this.ShouldSpawnHitmarker = spawnHitmarker;
			this.ShouldSpawnBloodDecal = spawnBloodDecal;
			this.SourcePosition = sourcePosition;
			this.TargetPosition = targetPosition;
			this.TargetHitbox = targetHitbox;
			this.WeaponSound = weaponSound;
			this.Direction = direction;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerShoot)handler).OnShoot(this);
		}
	}

	public class Player106CreatePortalEvent : PlayerEvent
	{
		public Vector Position { get; set; }

		public Player106CreatePortalEvent(Player player, Vector position) : base(player)
		{
			this.Position = position;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler106CreatePortal)handler).On106CreatePortal(this);
		}
	}

	public class Player106TeleportEvent : PlayerEvent
	{
		public Vector Position { get; set; }

		public Player106TeleportEvent(Player player, Vector position) : base(player)
		{
			this.Position = position;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler106Teleport)handler).On106Teleport(this);
		}
	}

	public class PlayerElevatorUseEvent : PlayerEvent
	{
		public Elevator Elevator { get; }
		public Vector ElevatorPosition { get; }
		public bool AllowUse { get; set; }

		public PlayerElevatorUseEvent(Player player, Elevator elevator, Vector elevatorPosition, bool allowUse) : base(player)
		{
			this.Elevator = elevator;
			this.ElevatorPosition = elevatorPosition;
			this.AllowUse = allowUse;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerElevatorUse)handler).OnElevatorUse(this);
		}
	}

	public class PlayerHandcuffedEvent : PlayerEvent
	{
		public bool Handcuffed { get; set; }

		public Player Owner { get; set; }

		public PlayerHandcuffedEvent(Player player, bool handcuffed, Player owner) : base(player)
		{
			this.Handcuffed = handcuffed;
			this.Owner = owner;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerHandcuffed)handler).OnHandcuffed(this);
		}
	}

	public class PlayerTriggerTeslaEvent : PlayerEvent
	{
		public TeslaGate TeslaGate { get; }
		public bool Triggerable { get; set; }

		public PlayerTriggerTeslaEvent(Player player, TeslaGate teslaGate, bool triggerable) : base(player)
		{
			this.TeslaGate = teslaGate;
			this.Triggerable = triggerable;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerTriggerTesla)handler).OnPlayerTriggerTesla(this);
		}
	}

	public class PlayerSCP914ChangeKnobEvent : PlayerEvent
	{
		public KnobSetting KnobSetting { get; set; }

		public PlayerSCP914ChangeKnobEvent(Player player, KnobSetting knobSetting) : base(player)
		{
			this.KnobSetting = knobSetting;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSCP914ChangeKnob)handler).OnSCP914ChangeKnob(this);
		}
	}

	public class PlayerRadioSwitchEvent : PlayerEvent
	{
		public RadioStatus ChangeTo { get; set; }

		public PlayerRadioSwitchEvent(Player player, RadioStatus changeTo) : base(player)
		{
			this.ChangeTo = changeTo;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerRadioSwitch)handler).OnPlayerRadioSwitch(this);
		}
	}

	public class PlayerMakeNoiseEvent : PlayerEvent
	{
		public PlayerMakeNoiseEvent(Player player) : base(player)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerMakeNoise)handler).OnMakeNoise(this);
		}
	}

	public class PlayerRecallZombieEvent: PlayerEvent
	{
		public Player Target { get; }

		public bool AllowRecall { get; set; }

		public PlayerRecallZombieEvent(Player player, Player target, bool allowRecall) : base(player)
		{
			this.Target = target;
			this.AllowRecall = allowRecall;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerRecallZombie)handler).OnRecallZombie(this);
		}
	}
	
	
	public class PlayerCallCommandEvent: PlayerEvent
	{
		public string ReturnMessage { get; set;}
		public string Command { get; }
		public PlayerCallCommandEvent(Player player, string command, string returnMessage) : base(player)
		{
			this.ReturnMessage = returnMessage;
			this.Command = command;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerCallCommand)handler).OnCallCommand(this);
		}
	}

	public class PlayerReloadEvent : PlayerEvent
	{
		public ItemType Weapon { get; }
		public int AmmoRemoved { get; set; }
		public int ClipAmmoCountAfterReload { get; set; }
		public int NormalMaxClipSize { get; }
		public int CurrentClipAmmoCount { get; }
		public int CurrentAmmoTotal { get; }
		public PlayerReloadEvent(Player player, ItemType weapon, int ammoRemoved, int clipAmmoCountAfterReload, int normalMaxClipSize, int currentClipAmmoCount, int currentAmmoTotal) : base(player)
		{
			this.Weapon = weapon;
			this.AmmoRemoved = ammoRemoved;
			this.ClipAmmoCountAfterReload = clipAmmoCountAfterReload;
			this.NormalMaxClipSize = normalMaxClipSize;
			this.CurrentClipAmmoCount = currentClipAmmoCount;
			this.CurrentAmmoTotal = currentAmmoTotal;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerReload)handler).OnReload(this);
		}
	}

	public class PlayerGrenadeExplosion : PlayerEvent
	{
		public PlayerGrenadeExplosion(Player thrower) : base(thrower)
		{
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGrenadeExplosion)handler).OnGrenadeExplosion(this);
		}
	}

	public class PlayerGrenadeHitPlayer : PlayerEvent
	{
		public Player Victim { get; }
		public PlayerGrenadeHitPlayer(Player thrower, Player victim) : base(thrower)
		{
			Victim = victim;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGrenadeHitPlayer)handler).OnGrenadeHitPlayer(this);
		}
	}

	public class PlayerGeneratorUnlockEvent : PlayerEvent
	{
		public Generator Generator { get; }
		public bool Allow { get; set; }

		public PlayerGeneratorUnlockEvent(Player player, Generator generator, bool allow) : base(player)
		{
			Generator = generator;
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGeneratorUnlock)handler).OnGeneratorUnlock(this);
		}
	}

	public class PlayerGeneratorAccessEvent : PlayerEvent
	{
		public Generator Generator { get; }
		public bool Allow { get; set; }

		public PlayerGeneratorAccessEvent(Player player, Generator generator, bool allow) : base(player)
		{
			Generator = generator;
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGeneratorAccess)handler).OnGeneratorAccess(this);
		}
	}

	public class PlayerGeneratorInsertTabletEvent : PlayerEvent
	{
		public Generator Generator { get; }
		public bool Allow { get; set; }
		public bool RemoveTablet { get; set; }

		public PlayerGeneratorInsertTabletEvent(Player player, Generator generator, bool allow, bool removeTablet) : base(player)
		{
			Generator = generator;
			Allow = allow;
			RemoveTablet = removeTablet;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGeneratorInsertTablet)handler).OnGeneratorInsertTablet(this);
		}
	}

	public class PlayerGeneratorEjectTabletEvent : PlayerEvent
	{
		public Generator Generator { get; }
		public bool Allow { get; set; }
		public bool SpawnTablet { get; set; }

		public PlayerGeneratorEjectTabletEvent(Player player, Generator generator, bool allow, bool spawnTablet) : base(player)
		{
			Generator = generator;
			Allow = allow;
			SpawnTablet = spawnTablet;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGeneratorEjectTablet)handler).OnGeneratorEjectTablet(this);
		}
	}

	public class Player079DoorEvent : PlayerEvent
	{
		public Door Door { get; }
		public bool Allow { get; set; }
		public float APDrain { get; set; }

		public Player079DoorEvent(Player player, Door door, bool allow, float apDrain) : base(player)
		{
			Door = door;
			Allow = allow;
			APDrain = apDrain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079Door)handler).On079Door(this);
		}
	}

	public class Player079LockEvent : PlayerEvent
	{
		public Door Door { get; }
		public bool Allow { get; set; }
		public float APDrain { get; set; }

		public Player079LockEvent(Player player, Door door, bool allow, float apDrain) : base(player)
		{
			Door = door;
			Allow = allow;
			APDrain = apDrain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079Lock)handler).On079Lock(this);
		}
	}

	public class Player079ElevatorEvent : PlayerEvent
	{
		public Elevator Elevator { get; }
		public bool Allow { get; set; }
		public float APDrain { get; set; }

		public Player079ElevatorEvent(Player player, Elevator elevator, bool allow, float apDrain) : base(player)
		{
			Elevator = elevator;
			Allow = allow;
			APDrain = apDrain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079Elevator)handler).On079Elevator(this);
		}
	}

	public class Player079TeslaGateEvent : PlayerEvent
	{
		public TeslaGate TeslaGate { get; }
		public bool Allow { get; set; }
		public float APDrain { get; set; }

		public Player079TeslaGateEvent(Player player, TeslaGate teslaGate, bool allow, float apDrain) : base(player)
		{
			TeslaGate = teslaGate;
			Allow = allow;
			APDrain = apDrain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079TeslaGate)handler).On079TeslaGate(this);
		}
	}

	public class Player079AddExpEvent : PlayerEvent
	{
		public ExperienceType ExperienceType { get; }
		public float ExpToAdd { get; set; }

		public Player079AddExpEvent(Player player, ExperienceType experienceType, float expToAdd) : base(player)
		{
			ExperienceType = experienceType;
			ExpToAdd = expToAdd;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079AddExp)handler).On079AddExp(this);
		}
	}

	public class Player079LevelUpEvent : PlayerEvent
	{
		public Player079LevelUpEvent(Player player) : base(player) { }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079LevelUp)handler).On079LevelUp(this);
		}
	}

	public class Player079UnlockDoorsEvent : PlayerEvent
	{
		public bool Allow { get; set; }

		public Player079UnlockDoorsEvent(Player player, bool allow) : base(player)
		{
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079UnlockDoors)handler).On079UnlockDoors(this);
		}
	}

	public class Player079CameraTeleportEvent : PlayerEvent
	{
		public Vector Camera { get; set; }
		public bool Allow { get; set; }
		public float APDrain { get; set; }

		public Player079CameraTeleportEvent(Player player, Vector camera, bool allow, float apDrain) : base(player)
		{
			Camera = camera;
			Allow = allow;
			APDrain = apDrain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079CameraTeleport)handler).On079CameraTeleport(this);
		}
	}

	public class Player079StartSpeakerEvent : PlayerEvent
	{
		public Room Room { get; }
		public bool Allow { get; set; }
		public float APDrain { get; set; }

		public Player079StartSpeakerEvent(Player player, Room room, bool allow, float apDrain) : base(player)
		{
			Room = room;
			Allow = allow;
			APDrain = apDrain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079StartSpeaker)handler).On079StartSpeaker(this);
		}
	}

	public class Player079StopSpeakerEvent : PlayerEvent
	{
		public Room Room { get; }
		public bool Allow { get; set; }

		public Player079StopSpeakerEvent(Player player, Room room, bool allow) : base(player)
		{
			Room = room;
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079StopSpeaker)handler).On079StopSpeaker(this);
		}
	}

	public class Player079LockdownEvent : PlayerEvent
	{
		public Room Room { get; }
		public bool Allow { get; set; }
		public float APDrain { get; set; }

		public Player079LockdownEvent(Player player, Room room, bool allow, float apDrain) : base(player)
		{
			Room = room;
			Allow = allow;
			APDrain = apDrain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079Lockdown)handler).On079Lockdown(this);
		}
	}

	public class Player079ElevatorTeleportEvent : PlayerEvent
	{
		public Vector Camera { get; }
		public Elevator Elevator { get; }
		public bool Allow { get; set; }
		public float APDrain { get; set; }

		public Player079ElevatorTeleportEvent(Player player, Vector camera, Elevator elevator, bool allow, float apDrain) : base(player)
		{
			Camera = camera;
			Elevator = elevator;
			Allow = allow;
			APDrain = apDrain;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandler079ElevatorTeleport)handler).On079ElevatorTeleport(this);
		}
	}
}
