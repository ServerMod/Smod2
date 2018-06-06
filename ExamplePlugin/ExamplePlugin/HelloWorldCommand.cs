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
			// This prints when someone types HELP HELLO
			return "Prints hello world";
		}

		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "HELLO";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			// This will print 3 lines in console.
			return new string[] { "Hello World!", "My name is example plugin.", "thank you for listening, good bye!" };
		}
	}
}
