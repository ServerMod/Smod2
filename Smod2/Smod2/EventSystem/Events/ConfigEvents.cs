using Smod2.API;
using Smod2.EventHandlers;
using System.Collections.Generic;

namespace Smod2.Events
{
	public class SetConfigEvent : Event
	{
		public string Key { get; }
		public object Value { get; set; }
		public object DefaultValue { get; }

		public SetConfigEvent(string key, object value, object def)
		{
			this.Key = key;
			this.Value = value;
			this.DefaultValue = def;
		}

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerSetConfig)handler).OnSetConfig(this);
		}
	}
}
