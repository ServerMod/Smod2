using Smod2;
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
		SmodMinor = 0,
		SmodRevision = 0
		)]
	class ExamplePlugin : Plugin
	{
		// Hooks to events
		[EventPipe("dev.plugin.OnNaeNaeDeath")]
		private void Test(Player player, int whips, int naes)
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

		public override void OnDisable()
		{
			this.Info(this.Details.name + " was disabled ):");
		}

		public override void OnEnable()
		{
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
	}
}
