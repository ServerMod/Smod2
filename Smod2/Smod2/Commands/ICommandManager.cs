namespace Smod2.Commands
{
	public interface ICommandManager
	{
		bool RegisterCommand(Plugin plugin, string command, ICommandHandler handler);
		void UnregisterCommands(Plugin plugin);
		string[] CallCommand(ICommandSender sender, string command, string[] args);
	}
}
