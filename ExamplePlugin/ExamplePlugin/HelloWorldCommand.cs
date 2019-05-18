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
			// Checks if the sender has permission to use this command, commands through the server console are always allowed
			if (sender is Player player)
			{
				if (!player.HasPermission("exampleplugin.helloworld"))
				{
					return new[] { "You don't have permission to use that command." };
				}
			}
			// This will print 3 lines in console.
			return new string[] { "Hello World!", "My name is example plugin.", "thank you for listening, good bye!" };
		}
	}
}
