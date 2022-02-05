namespace Smod2.Commands
{
	public interface ICommandHandler
	{
		string[] OnCall(ICommandSender sender, string[] args);
		string GetUsage();
		string GetCommandDescription();
	}
}
