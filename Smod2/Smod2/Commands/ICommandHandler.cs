namespace Smod2.Commands
{
	public interface ICommandHandler
	{
		void OnCall(ICommandManager manager, string[] args);
		string GetUsage();
		string GetCommandDescription();
	}
}
