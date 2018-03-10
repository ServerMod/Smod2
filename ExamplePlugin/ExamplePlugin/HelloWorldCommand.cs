using Smod2.Commands;

namespace ExamplePlugin
{
	class HelloWorldCommand : ICommandHandler
	{
		private ExamplePlugin plugin;
		public HelloWorldCommand(ExamplePlugin plugin)
		{
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return "Prints hello world";
		}

		public string GetUsage()
		{
			return "";
		}

		public void OnCall(ICommandManager manger, string[] args)
		{
			plugin.Info("Hello world!");
		}
	}
}
