using System;
using System.Reflection;

namespace Smod2.Piping
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyPipe : MemberPipe
	{
		private PropertyInfo info;

		public override Type Type { get; protected set; }
		public object Value
		{
			get
			{
				CheckInit();
				if (!Gettable)
				{
					throw new InvalidOperationException($"Cannot get ungettable property pipe: {(info.DeclaringType == null ? info.Name : info.DeclaringType.FullName + "." + info.Name)}");
				}

				return info.GetValue(Source);
			}
			set
			{
				CheckInit();
				if (!Settable)
				{
					throw new InvalidOperationException($"Cannot set unsettable property pipe: {(info.DeclaringType == null ? info.Name : info.DeclaringType.FullName + "." + info.Name)}");
				}

				info.SetValue(Source, value);
			}
		}

		public bool Gettable { get; private set; }
		public bool Settable { get; private set; }

		public PropertyPipe() : this(true, true) { }
		public PropertyPipe(bool gettable, bool settable)
		{
			Gettable = gettable;
			Settable = settable;
		}

		internal void Init(Plugin source, PropertyInfo info)
		{
			this.info = info;

			Type = info.PropertyType;
			Gettable = Gettable && info.GetMethod != null;
			Settable = Settable && info.SetMethod != null;

			base.Init(source, info);
		}
	}
}
