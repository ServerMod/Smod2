
using Smod2.API;

namespace Smod2.Events
{
	public interface IEventStartCountdown : IEvent //Before the countdown is started
	{
		void OnStartCountdown(float timeLeft, bool isResumed);
	}
	public interface IEventStopCountdown : IEvent //Before the countdown is stopped
	{
		void OnStopCountdown(float timeLeft);
	}
	public interface IEventDetonate : IEvent //Before the nuke detonates
	{
		void OnDetonate();
	}
	public interface IEventRearmed : IEvent //Currently not implemented, can't find the correct location for it
	{
		void OnRearmed(float timeLeft);
	}
}
