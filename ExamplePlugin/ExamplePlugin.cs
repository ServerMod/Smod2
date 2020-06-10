using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.Lang;
using Smod2.Piping;

namespace ExamplePlugin
{
	[PluginDetails(
		author = "Courtney",
		name = "Test",
		description = "Example plugin",
		id = "courtney.example.plugin",
		configPrefix = "ep",
		langFile = "exampleplugin",
		version = "1.0",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0
		)]
	public class ExamplePlugin : Plugin
	{
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
		
		// Grabs the "DamageMultiplier" field from the plugin with the ID "dev.plugin", given that the field is a pipe
		[PipeLink("dev.plugin", "DamageMultiplier")]
		private FieldPipe<float> damageMultiplier;

		// Registers config setting EP_MY_AWESOMENESS_SCORE with a default of 1 on initialization
		[ConfigOption]
		public readonly float myAwesomenessScore = 1f;

		// First bool indicates if the variable is the primary user (unless another plugin is using the config, this should be true). Second bool indicates if the variable is a random config.
		[ConfigOption(true, true)]
		public readonly LiveConfig<int> lottoItemCount = new LiveConfig<int>(1);

		// Registers lang setting CONFIG_VALUE in exampleplugin with a default of "Config value: " on initialization
		[LangOption] 
		public readonly string _configValue = "Config value: "; 

		public override void OnDisable()
		{
			this.Info(this.Details.name + " was disabled ):");
		}

		public override void OnEnable()
		{
			// Sets the pipe field to 0.1 if it exists. Pipes are not accessible in register.
			if (damageMultiplier != null)
			{
				damageMultiplier.Value = 0.1f;
			}

			this.Info(this.Details.name + " has loaded :)");
		}
		
		public override void Register()
		{
			killChance = 0.2f;

			// Registers a permissions handler, this is NOT required for checking permissions of players or adding default permissions
			// Use this if you are making a permission plugin
			this.RegisterPermissionsHandler(new PermissionHandler());
			// Register multiple events
			this.AddEventHandlers(new RoundEventHandler(this));
			// Register single event with priority (need to specify the handler type)
			this.AddEventHandler(typeof(IEventHandlerPlayerPickupItem), new LottoItemHandler(this), Priority.High);
			// Register Command(s)
			this.AddCommand("hello", new HelloWorldCommand(this));
			// Registers config at runtime (in this case it is in Register, so it is on initialization)
			this.AddConfig(new ConfigSetting("myConfigKey", "MyDefaultValue", true, "This is a description"));
			// Register lang at runtime (in this case it is in Register, so it is on initialization)
			this.AddTranslation(new LangSetting("myLangKey", "MyDefaultValue", "exampleplugin"));
			// Sets a permission node as a default permission meaning all players will have it unless overridden by a permission plugin
			this.AddDefaultPermission("exampleplugin.lottoitem");
		}
		
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

		// Declares a method usable to other plugins via piping.
		[PipeMethod]
		public bool GiveLottoItem(Player player)
		{
			// Makes sure the player is playing on a team that can have items
			if (player.TeamRole.Team == TeamType.SPECTATOR || 
				player.TeamRole.Team == TeamType.NONE ||
				player.TeamRole.Team == TeamType.SCP)
			{
				return false;
			}

			// Checks if the player has permission to receive items
			if (!player.HasPermission("exampleplugin.lottoitem"))
			{
				return false;
			}

			// Spawns a coin at the feet of the player
			Server.Map.SpawnItem(ItemType.COIN, player.GetPosition(), Vector.Zero);
			player.PersonalBroadcast(5, "A coin has been spawned at your feet. Pick it up for a chance to get a Micro HID!", false);

			// Invokes the OnGiveLottoItem event which other plugins may hook into, and includes the player in it
			InvokeEvent("OnGiveLottoItem", player);

			return true;
		}
	}
}
