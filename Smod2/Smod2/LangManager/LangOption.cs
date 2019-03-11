using System;

namespace Smod2.Lang
{
	[AttributeUsage(AttributeTargets.Field)]
	public class LangOption : Attribute
	{
		public string Key { get; }
		
		public LangOption() { }
		public LangOption(string key)
		{
			Key = key ?? throw new ArgumentNullException(nameof(key));
		}
	}
}