using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smod2.Commands
{
	public abstract class CommandManager
	{
		public abstract Boolean RegisterCommand(Plugin plugin, String command, ICommandHandler handler);
		public abstract void CallCommand(String command, String[] args);
	}
}
