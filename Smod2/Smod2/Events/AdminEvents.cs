
using Smod2.API;

namespace Smod2.Events
{
	public enum AuthType
	{
		PASSWORD,
		STAFF,
		PROJECT_MANAGMENT
	}
	public interface IEventAdminQuery : IEvent
	{
		void OnAdminQuery(Player admin, string adminIp, string query, out string queryOutput);
	}
	public interface IEventAuthCheck : IEvent
	{
		void OnAuthCheck(Player admin, AuthType authType, string entered_password, string server_password, bool allowOverwrite, out bool allowOutput);
	}

}
