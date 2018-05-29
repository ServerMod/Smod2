using System.Collections.Generic;
using Smod2.API;

namespace Smod2.Events.Admin
{
	public class AdminQueryEvent : Event
	{
		Player Admin { get; set; }
		string Query { get; set; }

		public override void ExecuteHandlers()
		{
			this.GetEventHandlers<IEventHandlerAdminQuery>().ForEach((handler) => { handler.OnAdminQuery(this); });
		}

		public override List<IEventHandlerAdminQuery> GetEventHandlers<IEventHandlerAdminQuery>()
		{
			return EventManager.Manager.GetEventHandlers<IEventHandlerAdminQuery>();
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

		public override void ExecuteHandlers()
		{
			this.GetEventHandlers<IEventHandlerAuthCheck>().ForEach((handler) => { handler.OnAuthCheck(this); });
		}

		public override List<IEventHandlerAuthCheck> GetEventHandlers<IEventHandlerAuthCheck>()
		{
			return EventManager.Manager.GetEventHandlers<IEventHandlerAuthCheck>();
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

		public override void ExecuteHandlers()
		{
			this.GetEventHandlers<IEventHandlerBan>().ForEach((handler) => { handler.OnBan(this); });
		}

		public override List<IEventHandlerBanEvent> GetEventHandlers<IEventHandlerBanEvent>()
		{
			return EventManager.Manager.GetEventHandlers<IEventHandlerBanEvent>();
		}
	}


}
