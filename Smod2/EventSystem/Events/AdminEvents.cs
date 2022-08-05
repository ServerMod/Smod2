using Smod2.API;
using Smod2.EventHandlers;

namespace Smod2.Events
{
	public class AdminQueryEvent : Event
	{
		public Player Admin { get; set; }
		public string Query { get; set; }
		public string Output { get; set; }
		public bool Handled { get; set; }
		public bool Successful { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerAdminQuery)handler).OnAdminQuery(this);
		}
	}

	public class BanEvent : Event
	{
		public Player Player { get; set; }
		public string Issuer { get; set; }
		public long Duration { get; set; }
		public string Reason { get; set; }
		public string Result { get; set; }
		public bool AllowBan { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerBan)handler).OnBan(this);
		}
	}
}
