using Smod.TestPlugin;
using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ExamplePlugin
{
	[PluginDetails(
		author = "Courtney",
		name = "Test",
		description = "Example plugin",
		id = "courtney.example.plugin",
		version = "3.0",
		SmodMajor = 3,
		SmodMinor = 0,
		SmodRevision = 0
		)]
	class ExamplePlugin : Plugin
	{
		public override void OnDisable()
		{
		}

		public override void OnEnable()
		{
			this.Info("Example Plugin has loaded :)");
			this.Info("Config value: " + this.GetConfigString("test"));
		}

		public override void Register()
		{
			// Register Events
			this.AddEventHandlers(new RoundEventHandler(this));
			// Register with priority (need to specify the handler type
            this.AddEventHandler(typeof(IEventHandlerPlayerPickupItem), new LottoItemHandler(this), Priority.Highest);
			// Register Commands
			this.AddCommand("hello", new HelloWorldCommand(this));
			// Register config settings
			this.AddConfig(new Smod2.Config.ConfigSetting("test", "yes", Smod2.Config.SettingType.STRING, true, "test"));
		}
	}
}
