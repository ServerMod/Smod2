using System;
using System.Linq;
using System.Reflection;

namespace Smod2.Piping
{
	[AttributeUsage(AttributeTargets.Method)]
	public class MethodPipe : MemberPipe
	{
		private MethodInfo info;
		private MethodPipeParameter[] pipeParameters;

		public override Type Type { get; protected set; }

		public object Invoke(params object[] parameters)
		{
			return info.Invoke(Source, parameters);
		}

		public MethodPipeParameter[] GetParameters() => pipeParameters.ToArray();

		internal void Init(Plugin source, MethodInfo info)
		{
			this.info = info;

			ParameterInfo[] parameters = info.GetParameters();
			pipeParameters = new MethodPipeParameter[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				pipeParameters[i] = new MethodPipeParameter(parameters[i]);
			}

			Type = info.ReturnType;

			base.Init(source, info);
		}
	}
}
