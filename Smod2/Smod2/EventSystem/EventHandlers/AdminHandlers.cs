using Smod2.Events;

namespace Smod2.EventHandlers
{
	public interface IEventHandlerAdminQuery : IEventHandler
	{
		void OnAdminQuery(AdminQueryEvent ev);
	}
	public interface IEventHandlerAuthCheck : IEventHandler
	{
		void OnAuthCheck(AuthCheckEvent ev);
	}
	public interface IEventHandlerBan : IEventHandler
	{
		void OnBan(BanEvent ev);
	}
}
