using Smod2.API;

namespace Smod2.API
{
	public abstract class Enviroment
	{
		public abstract bool WarheadDetonated { get; }
		public abstract bool LCZDecontaminated { get; }
	}
}
