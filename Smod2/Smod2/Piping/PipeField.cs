using System;
using System.Reflection;

namespace Smod2.Piping
{
	[AttributeUsage(AttributeTargets.Field)]
	public class PipeField : Attribute
	{
		public bool Readonly { get; }

		public PipeField() : this(false) { }
		public PipeField(bool @readonly)
		{
			Readonly = @readonly;
		}
	}

	public class FieldPipe : MemberPipe
	{
		protected readonly FieldInfo info;
		public object Value
		{
			get => info.GetValue(instance);
			set
			{
				if (Readonly)
				{
					throw new InvalidOperationException($"Cannot set readonly field pipe: {(info.DeclaringType == null ? info.Name : info.DeclaringType.FullName + "." + info.Name)}");
				}
				
				info.SetValue(instance, value);
			}
		}
		
		public bool Readonly { get; }
		
		internal FieldPipe(Plugin source, FieldInfo info, PipeField pipe) : base(source, info, info.IsStatic)
		{
			Readonly = pipe.Readonly;
			
			this.info = info;

			Type = info.FieldType;
			Readonly = Readonly && info.IsInitOnly;

			if (!info.IsPublic)
			{
				PluginManager.Manager.Logger.Warn("PIPE_MANAGER", $"Pipe field {Name} in {Source.Details.id} is not public. This is bad practice.");
			}
		}
	}

	public class FieldPipe<T> : FieldPipe
	{
		public new T Value
		{
			get => (T)base.Value;
			set => base.Value = value;
		}
		
		internal FieldPipe(Plugin source, FieldInfo info, PipeField pipe) : base(source, info, pipe) { }

		public static implicit operator T(FieldPipe<T> pipe) => pipe.Value;
	}
}
