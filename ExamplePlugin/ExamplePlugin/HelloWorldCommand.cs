using Smod2.Commands;
using Smod2.API;

namespace ExamplePlugin
{
	class HelloWorldCommand : ICommandHandler
	{
		private readonly ExamplePlugin plugin;

		public HelloWorldCommand(ExamplePlugin plugin)
		{
			//Constructor passing plugin reference to this class
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
			//Will be null if command was called by server console
			Player caller = sender as Player;
			// This will print 3 lines in console.
			return new string[] { "Hello World!", "My name is example plugin.", "thank you for listening, good bye!" };
		}
	}
}
