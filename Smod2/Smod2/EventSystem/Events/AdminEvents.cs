using Smod2.API;
using Smod2.EventHandlers;

namespace Smod2.Events
{
	public class AdminQueryEvent : Event
	{
		Player Admin { get; set; }
		string Query { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerAdminQuery)handler).OnAdminQuery(this);
		}
	}

	public class AuthCheckEvent : Event
	{
		Player Admin { get; set; }
		AuthType AuthType { get; set; }
		string EnteredPassword { get; set; }
		string ServerPassword { get; set; }
		bool AllowOverwrite { get; set; }
		bool AllowOutput { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerAuthCheck)handler).OnAuthCheck(this);
		}
	}

	public class BanEvent : Event
	{
		Player Player { get; set; }
		Player Admin { get; set; }
		int Duration { get; set; }
		string Reason { get; set; }
		string Result { get; set; }
		bool AllowBan { get; set; }
		string ResultOutput { get; set; }

		public override void ExecuteHandler(IEventHandler handler)
		{
			((IEventHandlerBan)handler).OnBan(this);
		}

	}


}
