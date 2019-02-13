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

		public object Invoke() => Invoke(null);
		public object Invoke(params object[] parameters)
		{
			try
			{
				return info.Invoke(Source, parameters);
			}
			catch (TargetParameterCountException e)
			{
				if (e.TargetSite.Name != "ConvertValues")
				{
					throw;
				}

				throw new TargetParameterCountException($"{pipeParameters.Length} parameters were expected, but {parameters?.Length ?? 0} were supplied.", e);
			}
			catch (ArgumentException e)
			{
				if (e.TargetSite.Name != "CheckValue")
				{
					throw;
				}

				throw new ArgumentException("Parameter type mismatch. Check the plugin which hosts the method pipe and make sure your arguments types are correct.", e);
			}
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

			base.Init(info.IsStatic ? null : source, info);
		}
	}
}
