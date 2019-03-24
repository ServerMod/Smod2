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
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentException("Lang keys cannot be null, whitespace, or empty.", nameof(key));
			}
			
			Key = key;
		}
	}
}
