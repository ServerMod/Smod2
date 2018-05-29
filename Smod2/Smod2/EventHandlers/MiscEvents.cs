
using Smod2.API;

namespace Smod2.Events
{
	public interface IEventNicknameSet : IEventHandler
	{
		void OnNicknameSet(Player player, string nickname, out string nicknameOutput);
	}
	public interface IEvent914Activate : IEventHandler
	{
		void On914Activate(KnobSetting knobSetting, object[] inputs, Vector intakePos, Vector outputPos);
	}
	public interface IEventPocketDimensionExit : IEventHandler
	{
		void OnPocketDimensionExit(Vector[] possibleExits, out Vector[] possibleExitsOutput);
	}
}
