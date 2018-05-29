using System.Collections.Generic;

namespace Smod2.Events
{
	public abstract class Event
	{
		public abstract List<T> GetEventHandlers<T>();

		public abstract void ExecuteHandlers();
	}
}
