using Smod2.API;
using Smod2.EventHandlers;
using System.Collections.Generic;

namespace Smod2.Events
{
	public abstract class PlayerEvent : Event
	{
		protected PlayerEvent(Player player)
		{
			this.Player = player;
		}

		public Player Player { get; }
	}

	public class PlayerHurtEvent : PlayerEvent
	{
		public PlayerHurtEvent(Player player, Player attacker, float damage, DamageType damageType) : base(player)
		{
			this.Attacker = attacker;
			Damage = damage;
			DamageType = damageType;
		}

		public Player Attacker { get; }
		public float Damage { get; set; }
		public DamageType DamageType { get; }

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
		public DamageType DamageTypeVar { get; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerDie)handler).OnPlayerDie(this);
		}
	}

	public abstract class PlayerItemEvent : PlayerEvent
	{
		protected PlayerItemEvent(Player player, Item item, ItemType change, bool allow) : base(player)
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

	public class PlayerDropAllItemsEvent : PlayerEvent
	{
		public bool Allow { get; set; }

		public PlayerDropAllItemsEvent(Player player, bool allow = true) : base(player)
		{
			this.Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerDropAllItems)handler).OnPlayerDropAllItems(this);
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
		public PlayerInitialAssignTeamEvent(Player player, TeamType team) : base(player)
		{
			Team = team;
		}

		public TeamType Team { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerInitialAssignTeam)handler).OnAssignTeam(this);
		}
	}

	public class PlayerCheckEscapeEvent : PlayerEvent
	{
		public bool AllowEscape { get; set; }
		public RoleType ChangeRole { get; set; }

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
		/// <summary>
		/// Called when a player role is set, on respawn or otherwise.
		/// </summary>
		/// <param name="player">The player whose role has changed.</param>
		/// <param name="newRole">The role the player is about to get, use roleType to change it.</param>
		/// <param name="roleType">The role type the player is about to get.</param>
		public PlayerSetRoleEvent(Player player, Role newRole, RoleType roleType) : base(player)
		{
			this.NewRole = newRole;
			this.RoleType = roleType;
		}

		public RoleType RoleType { get; set; }

		public Role NewRole { get; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			Player.CallSetRoleEvent = false;
			((IEventHandlerSetRole)handler).OnSetRole(this);
			Player.CallSetRoleEvent = true;
		}
	}

	public class PlayerSetInventoryEvent : PlayerEvent
	{
		/// <summary>
		/// Triggers before a player gets their new inventory items after a role change.
		/// </summary>
		/// <param name="player">The player whose role has changed.</param>
		/// <param name="previousRole">The role the player had before the role change, check the player for the new role.</param>
		/// <param name="items">The items which will be given to the player.</param>
		/// <param name="ammo">The ammo type and amount which will be given to the player.</param>
		/// <param name="dropExistingItems">Whether to drop or simply delete the existing inventory items.</param>
		public PlayerSetInventoryEvent(Player player, Role previousRole, List<ItemType> items, Dictionary<AmmoType, ushort> ammo, bool dropExistingItems) : base(player)
		{
			this.PreviousRole = previousRole;
			this.Items = items;
			this.Ammo = ammo;
			this.DropExistingItems = dropExistingItems;
		}

		public List<ItemType> Items { get; set; }

		public Dictionary<AmmoType, ushort> Ammo { get; set; }

		public bool DropExistingItems { get; set; }

		public Role PreviousRole { get; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSetInventory)handler).OnSetInventory(this);
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
		public Door Door { get; }
		public bool Allow { get; set; }
		public bool Destroy { get; set; }

		public PlayerDoorAccessEvent(Player player, Door door) : base(player)
		{
			this.Door = door;
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

		public bool Allow { get; set; }

		public PlayerPocketDimensionEnterEvent(Player player, float damage, Vector lastPosition, Vector targetPosition, Player attacker, bool allow) : base(player)
		{
			Damage = damage;
			LastPosition = lastPosition;
			TargetPosition = targetPosition;
			Attacker = attacker;
			Allow = allow;
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
		public GrenadeType GrenadeType { get; set; }
		public Vector Direction { get; set; }
		public bool SlowThrow { get; set; }
		public bool Allow { get; set; }

		public PlayerThrowGrenadeEvent(Player player, GrenadeType grenadeType, Vector direction, bool slowThrow, bool allow) : base(player)
		{
			this.GrenadeType = grenadeType;
			this.Direction = direction;
			this.SlowThrow = slowThrow;
			this.Allow = allow;
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
		public RoleType RoleID { get; set; }
		public Vector Position { get; set; }
		public Vector Rotation { get; set; }
		public Player Attacker { get; }
		public DamageType DamageType { get; }

		public PlayerSpawnRagdollEvent(Player player, RoleType roleID, Vector position, Vector rotation, Player attacker, DamageType damageType) : base(player)
		{
			this.RoleID = roleID;
			this.Position = position;
			this.Rotation = rotation;
			this.Attacker = attacker;
			this.DamageType = damageType;
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

	public class PlayerMedicalUseEvent : PlayerEvent
	{
		public float Health { get; set; }
		public float ArtificialHealth { get; set; }
		public float HealthRegenAmount { get; set; }
		public float HealthRegenSpeedMultiplier { get; set; }
		public float Stamina { get; set; }
		public MedicalItem MedicalItem { get; set; }

		public PlayerMedicalUseEvent(Player player, float health, float artificialHealth, float stamina, MedicalItem medicalItem, float healthRegenAmount, float healthRegenSpeedMultiplier) : base(player)
		{
			Health = health;
			ArtificialHealth = artificialHealth;
			Stamina = stamina;
			MedicalItem = medicalItem;
			HealthRegenAmount = healthRegenAmount;
			HealthRegenSpeedMultiplier = healthRegenSpeedMultiplier;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerMedicalUse)handler).OnMedicalUse(this);
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
		public bool Allow { get; set; }

		public Player Disarmer { get; }

		public PlayerHandcuffedEvent(Player player, Player disarmer, bool allow = true) : base(player)
		{
			this.Allow = allow;
			this.Disarmer = disarmer;
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
		public bool Allow { get; set; }

		public PlayerMakeNoiseEvent(Player player, bool allow) : base(player)
		{
			this.Allow = allow;
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
		public Weapon Weapon { get; }
		public int AmmoRemoved { get; }
		public int ClipAmmoCountAfterReload { get; }
		public int NormalMaxClipSize { get; }
		public int CurrentClipAmmoCount { get; }
		public int CurrentAmmoTotal { get; }
		public bool Allow { get; set; }

		public PlayerReloadEvent(Player player, Weapon weapon, int ammoRemoved, int clipAmmoCountAfterReload, int normalMaxClipSize, int currentClipAmmoCount, int currentAmmoTotal, bool allow) : base(player)
		{
			this.Weapon = weapon;
			this.AmmoRemoved = ammoRemoved;
			this.ClipAmmoCountAfterReload = clipAmmoCountAfterReload;
			this.NormalMaxClipSize = normalMaxClipSize;
			this.CurrentClipAmmoCount = currentClipAmmoCount;
			this.CurrentAmmoTotal = currentAmmoTotal;
			this.Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerReload)handler).OnReload(this);
		}
	}

	public class PlayerGrenadeExplosion : PlayerEvent
	{
		public bool Allow { get; set; }
		public GrenadeType GrenadeType { get; }
		public Vector Position { get; set; }
		public PlayerGrenadeExplosion(Player thrower, GrenadeType grenadeType, Vector position, bool allow = true) : base(thrower)
		{
			this.Allow = allow;
			this.GrenadeType = grenadeType;
			this.Position = position;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGrenadeExplosion)handler).OnGrenadeExplosion(this);
		}
	}

	public class PlayerGrenadeHitPlayer : PlayerEvent
	{
		public Player Victim { get; }
		public GrenadeType GrenadeType { get; }
		public Vector ExplosionForce { get; set; }
		public float Damage { get; set; }

		public PlayerGrenadeHitPlayer(Player thrower, Player victim, GrenadeType type, Vector explosionForce, float damage) : base(thrower)
		{
			GrenadeType = type;
			ExplosionForce = explosionForce;
			Damage = damage;
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

	public class PlayerGeneratorLeverUsedEvent : PlayerEvent
	{
		public Generator Generator { get; }
		public bool Allow { get; set; }
		public bool Activated { get; set; }

		public PlayerGeneratorLeverUsedEvent(Player player, Generator generator, bool allow, bool activated) : base(player)
		{
			Generator = generator;
			Allow = allow;
			Activated = activated;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerGeneratorLeverUsed)handler).OnGeneratorLeverUsed(this);
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

	public class Scp096PanicEvent : PlayerEvent
	{
		public bool Allow { get; set; }
		public float PanicTime { get; set; }

		public Scp096PanicEvent(Player player, bool allow, float panicTime) : base(player)
		{
			Allow = allow;
			PanicTime = panicTime;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerScp096Panic)handler).OnScp096Panic(this);
		}
	}

	public class Scp096EnrageEvent : PlayerEvent
	{
		public bool Allow { get; set; }

		public Scp096EnrageEvent(Player player, bool allow) : base(player)
		{
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerScp096Enrage)handler).OnScp096Enrage(this);
		}
	}

	public class Scp096CooldownStartEvent : PlayerEvent
	{
		public bool Allow { get; set; }

		public Scp096CooldownStartEvent(Player player, bool allow) : base(player)
		{
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerScp096CooldownStart)handler).OnScp096CooldownStart(this);
		}
	}

	public class Scp096CooldownEndEvent : PlayerEvent
	{
		public bool Allow { get; set; }

		public Scp096CooldownEndEvent(Player player, bool allow) : base(player)
		{
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerScp096CooldownEnd)handler).OnScp096CooldownEnd(this);
		}
	}

	public class Scp096AddTargetEvent : PlayerEvent
	{
		public bool Allow { get; set; }
		public Player Target { get; }

		public Scp096AddTargetEvent(Player scp096, Player target, bool allow = true) : base(scp096)
		{
			Allow = allow;
			Target = target;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerScp096AddTarget)handler).OnScp096AddTarget(this);
		}
	}

	public sealed class PlayerLockerAccessEvent : PlayerEvent
	{
		public byte LockerId { get; }
		public byte ChamberId { get; }
		/// <summary>
		///	Is the same permissions for items.
		/// </summary>
		public string ChamberAccessToken { get; }
		/// <summary>
		///	true if the player is opening the locker; otherwise, false.
		/// </summary>
		public bool IsOpening { get; }
		public bool Allow { get; set; }

		public PlayerLockerAccessEvent(Player ply,
			byte lockerId, byte chamberId, string chamberAccessToken,
			 bool isOpening, bool allow) : base(ply)
		{
			LockerId = lockerId;
			ChamberId = chamberId;
			ChamberAccessToken = chamberAccessToken;
			IsOpening = isOpening;
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerLockerAccess)handler).OnPlayerLockerAccess(this);
		}
	}

	public class DisableStatusEffectEvent : PlayerEvent
	{
		public bool Allow { get; set; }
		public PlayerEffect PlayerEffect { get; }

		public DisableStatusEffectEvent(Player player, PlayerEffect effect, bool allow = true) : base(player)
		{
			PlayerEffect = effect;
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerDisableStatusEffect)handler).OnDisableStatusEffect(this);
		}
	}

	public class EarlyStatusEffectChangeEvent : PlayerEvent
	{
		public bool Allow { get; set; }
		public PlayerEffect PlayerEffect { get; }
		public float NewValue { get; set; }

		public EarlyStatusEffectChangeEvent(Player player, PlayerEffect effect, float newValue, bool allow = true) : base(player)
		{
			PlayerEffect = effect;
			Allow = allow;
			NewValue = newValue;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerEarlyStatusEffectChange)handler).OnEarlyStatusEffectChange(this);
		}
	}

	public class LateStatusEffectChangeEvent : PlayerEvent
	{
		public PlayerEffect PlayerEffect { get; }
		public float NewValue { get; set; }

		public LateStatusEffectChangeEvent(Player player, PlayerEffect effect, float newValue) : base(player)
		{
			PlayerEffect = effect;
			NewValue = newValue;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerLateStatusEffectChange)handler).OnLateStatusEffectChange(this);
		}
	}

	public class PlayerSCP268UseEvent : PlayerEvent
	{
		public Item SCP268 { get; }
		public bool Allow { get; set; }
		public float Cooldown { get; set; }

		public PlayerSCP268UseEvent(Player player, Item item, float cd, bool allow = true) : base(player)
		{
			Cooldown = cd;
			SCP268 = item;
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerSCP268Use)handler).OnPlayerSCP268Use(this);
		}
	}

	public class PlayerSCP207UseEvent : PlayerEvent
	{
		public Item SCP207 { get; }
		public bool Allow { get; set; }

		public PlayerSCP207UseEvent(Player player, Item item, bool allow = true) : base(player)
		{
			SCP207 = item;
			Allow = allow;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerPlayerSCP207Use)handler).OnPlayerSCP207Use(this);
		}
	}
}
