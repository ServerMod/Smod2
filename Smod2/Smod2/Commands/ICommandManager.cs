namespace Smod2.Commands
{
	public interface ICommandManager
	{
		bool RegisterCommand(Plugin plugin, string command, ICommandHandler handler);
		void UnregisterCommands(Plugin plugin);
		void CallCommand(string command, string[] args);
		void Write(string message);
	}
}
