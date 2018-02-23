using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smod2.Commands
{
	public interface ICommandHandler
	{
		void OnCall(String[] args);
		String GetUsage();
		String GetCommandDescription();
	}
}
