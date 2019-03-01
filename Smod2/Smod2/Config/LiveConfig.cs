using System;

namespace Smod2.Config
{
	public class LiveConfig
	{
		public string DefaultValue { get; }
		public string Key { get; private set; }
		public Plugin Owner { get; private set; }

		protected LiveConfig(string defaultValue)
		{
			DefaultValue = defaultValue;
		}
		
		internal void ManagerInit(string key, Plugin owner)
		{
			Key = key;
			Owner = owner;
		}
		
		// Checks key instead of reference
		public static bool operator ==(LiveConfig liveConfig1, LiveConfig liveConfig2) =>
			(liveConfig1?.Key ?? throw new ArgumentNullException(nameof(liveConfig1))) ==
			(liveConfig2?.Key ?? throw new ArgumentNullException(nameof(liveConfig2)));
		public static bool operator !=(LiveConfig liveConfig1, LiveConfig liveConfig2) => !(liveConfig1 == liveConfig2);
		
		public override bool Equals(object obj) =>
			!ReferenceEquals(null, obj) && 
			ReferenceEquals(this, obj);

		public override int GetHashCode() => Key?.GetHashCode() ?? 0;
	}
	
	public class LiveConfig<T> : LiveConfig
	{
		private readonly Type type;
		
		public T Value
		{
			get
			{
				if (Key == null)
				{
					throw new InvalidOperationException($"{nameof(ConfigManager)} has not initialized this live config.");
				}
				
				return (T) ConfigManager.Manager.typeGetters[type].Invoke(Owner, Key);
			}
		}
		
		public LiveConfig(string defaultValue) : base(defaultValue)
		{
			type = typeof(T);
		}

		public static implicit operator T(LiveConfig<T> config) => config.Value;
	}
}