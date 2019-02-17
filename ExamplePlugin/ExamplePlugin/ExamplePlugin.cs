﻿using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.Piping;

namespace ExamplePlugin
{
	[PluginDetails(
		author = "Courtney",
		name = "Test",
		description = "Example plugin",
		id = "courtney.example.plugin",
		version = "1.0",
		SmodMajor = 3,
		SmodMinor = 3,
		SmodRevision = 1
		)]
	public class ExamplePlugin : Plugin
	{
		// Hooks to event called by any plugin
		// Hooks can be private as well as public, but all other pipes must be public
		[PipeEvent("dev.plugin.OnEpicDeath")]
		private void OnEpicDeath(Player player, int whips, int naes)
		{
			if (naes > 2)
			{
				player.SendConsoleMessage("get nae nae'd");
			}

			if (whips > 1)
			{
				player.SendConsoleMessage("get whipped on");
			}
		}

		// Hooks to event called by specific plugins
		[PipeEvent("dev.plugin.OnWhip",
			"dev.plugin",
			"hoob.hud")]
		private void OnWhip(Player player, Player target)
		{
			player.SendConsoleMessage("You were whipped on by " + target.Name);
		}

		// Grabs the "DamageMultiplier" field from the plugin with the ID "dev.plugin", given that the field is a pipe
		[PipeLink("dev.plugin", "DamageMultiplier")]
		private FieldPipe<float> damageMultiplier;

		public override void OnDisable()
		{
			this.Info(this.Details.name + " was disabled ):");
		}

		public override void OnEnable()
		{
			// Sets the pipe field to 0.1. Pipes are not accessable in register.
			if (damageMultiplier != null)
			{
				damageMultiplier.Value = 0.1f;
			}

			this.Info(this.Details.name + " has loaded :)");
			this.Info("Config value: " + this.GetConfigString("myConfigKey"));
		}
		
		public override void Register()
		{
			// Register multiple events
			this.AddEventHandlers(new RoundEventHandler(this));
			// Register single event with priority (need to specify the handler type)
			this.AddEventHandler(typeof(IEventHandlerPlayerPickupItem), new LottoItemHandler(this), Priority.High);
			// Register Command(s)
			this.AddCommand("hello", new HelloWorldCommand(this));
			// Register config setting(s)
			this.AddConfig(new Smod2.Config.ConfigSetting("myConfigKey", "MyDefaultValue", Smod2.Config.SettingType.STRING, true, "This is a description"));
		}

		[PipeField]
		public float killChance;

		private float microChance;
		[PipeProperty]
		public float MicroChance
		{
			get
			{
				Info("Got MicroChance as " + microChance);
				return microChance;
			}
			set
			{
				Info("Set MicroChance to " + value);
				microChance = value;
			}
		}

		// Declares a method usable to other plugins via piping.
		[PipeMethod]
		public bool GiveLottoItem(Player player)
		{
			if (player.TeamRole.Team == Team.SPECTATOR || 
			    player.TeamRole.Team == Team.NONE ||
			    player.TeamRole.Team == Team.SCP)
			{
				return false;
			}

			Server.Map.SpawnItem(ItemType.COIN, player.GetPosition(), Vector.Zero);
			player.PersonalBroadcast(5, "A coin has been spawned at your feet. Pick it up for a chance to get a Micro HID!", false);
			InvokeEvent("OnGiveLottoItem", player);

			return true;
		}
	}
}
