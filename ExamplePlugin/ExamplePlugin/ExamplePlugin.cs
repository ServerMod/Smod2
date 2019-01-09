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
		version = "1.0",
		SmodMajor = 3,
		SmodMinor = 0,
		SmodRevision = 0
		)]
	class ExamplePlugin : Plugin
	{
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
			//Register multiple events with Low Priority
			this.AddEventHandlers(new MultipleEventsExample(this), Priority.Low);
			// Register with priority (need to specify the handler type)
			this.AddEventHandler(typeof(IEventHandlerPlayerPickupItem), new LottoItemHandler(this), Priority.High);
			// Register Commands
			this.AddCommand("hello", new HelloWorldCommand(this));
			// Register config settings
			this.AddConfig(new Smod2.Config.ConfigSetting("myConfigKey", "MyDefaultValue", Smod2.Config.SettingType.STRING, true, "This is a description"));
		}
	}
}
