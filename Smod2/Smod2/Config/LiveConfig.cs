using System;

namespace Smod2.Config
{
	public abstract class LiveConfig
	{
		public object DefaultValue { get; }
		public string Key { get; private set; }
		public Plugin Owner { get; private set; }

		protected LiveConfig(object defaultValue)
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
		private readonly Func<Plugin, string, object> getter;
		
		public new T DefaultValue { get; }
		
		public T Value
		{
			get
			{
				if (Key == null)
				{
					throw new InvalidOperationException($"{nameof(ConfigManager)} has not initialized this live config.");
				}
				
				return (T) getter.Invoke(Owner, Key);
			}
		}
		
		public LiveConfig(T defaultValue) : base(defaultValue)
		{
			Type type = typeof(T);
			if (!ConfigManager.Manager.typeGetters.ContainsKey(type))
			{
				throw new InvalidOperationException($"{type} is not a valid config type.");
			}
			
			getter = ConfigManager.Manager.typeGetters[type];
			DefaultValue = defaultValue;
		}

		public static implicit operator T(LiveConfig<T> config) => config.Value;
	}
}