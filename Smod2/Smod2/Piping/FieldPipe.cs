using System;
using System.Reflection;

namespace Smod2.Piping
{
	[AttributeUsage(AttributeTargets.Field)]
	public class FieldPipe : MemberPipe
	{
		private FieldInfo info;

		public override Type Type { get; protected set; }
		public object Value
		{
			get
			{
				CheckInit();

				return info.GetValue(Source);
			}
			set
			{
				CheckInit();
				if (Readonly)
				{
					throw new InvalidOperationException($"Cannot set field pipe {(info.DeclaringType == null ? info.Name : info.DeclaringType.FullName + "." + info.Name)}. It is readonly.");
				}

				info.SetValue(Source, value);
			}
		}
		
		public bool Readonly { get; private set; }

		public FieldPipe() : this(true) { }
		public FieldPipe(bool @readonly)
		{
			Readonly = @readonly;
		}

		internal void Init(Plugin source, FieldInfo info)
		{
			this.info = info;

			Type = info.FieldType;
			Readonly = Readonly && info.IsInitOnly;

			if (!info.IsPublic)
			{
				PluginManager.Manager.Logger.Warn("PIPE_MANAGER", $"Pipe field {Name} in {Source.Details.id} is not public. This is bad practice.");
			}

			base.Init(info.IsStatic ? null : source, info);
		}
	}
}
