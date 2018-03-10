namespace Smod2.Commands
{
	public interface ICommandManager
	{
		bool RegisterCommand(Plugin plugin, string command, ICommandHandler handler);
		void CallCommand(string command, string[] args);
		void Write(string message);
	}
}
