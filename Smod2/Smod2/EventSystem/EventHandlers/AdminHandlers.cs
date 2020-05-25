using System;
using Smod2.Events;

namespace Smod2.EventHandlers
{
	public interface IEventHandlerAdminQuery : IEventHandler
	{
		void OnAdminQuery(AdminQueryEvent ev);
	}
	[Obsolete("This event is never triggered and will be removed in the future")]
	public interface IEventHandlerAuthCheck : IEventHandler
	{
		void OnAuthCheck(AuthCheckEvent ev);
	}
	public interface IEventHandlerBan : IEventHandler
	{
		void OnBan(BanEvent ev);
	}
}
