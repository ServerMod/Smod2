
using Smod2.API;

namespace Smod2.Events
{
	public interface IEventStartCountdown : IEventHandler //Before the countdown is started
	{
		void OnStartCountdown(float timeLeft, bool isResumed);
	}
	public interface IEventStopCountdown : IEventHandler //Before the countdown is stopped
	{
		void OnStopCountdown(float timeLeft);
	}
	public interface IEventDetonate : IEventHandler //Before the nuke detonates
	{
		void OnDetonate();
	}
	public interface IEventRearmed : IEventHandler //Currently not implemented, can't find the correct location for it
	{
		void OnRearmed(float timeLeft);
	}
}
