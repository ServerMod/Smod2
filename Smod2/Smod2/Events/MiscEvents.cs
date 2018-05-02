
using Smod2.API;

namespace Smod2.Events
{
	public interface IEventNicknameSet : IEvent
	{
		void OnNicknameSet(Player player, string nickname, out string nicknameOutput);
	}
	public interface IEvent914Activate : IEvent
	{
		void On914Activate(KnobSetting knobSetting, object[] inputs, Vector intakePos, Vector outputPos);
	}
	public interface IEventPocketDimensionExit : IEvent
	{
		void OnPocketDimensionExit(Vector[] possibleExits, out Vector[] possibleExitsOutput);
	}
}
