using System;

namespace Smod2.Lang
{
	public class LangSetting
	{
		private string key;
		public string Key
		{
			get
			{
				return key;
			}
		}

		private string defaultText;
		public string Default
		{
			get
			{
				return defaultText;
			}
		}

		private string filename;
		public string Filename
		{
			get
			{
				return filename;
			}
		}

		public LangSetting(string key, string defaultText, string filename = "ServerMod")
		{
			this.key = (key ?? throw new ArgumentNullException(nameof(key))).ToUpper();
			this.defaultText = defaultText ?? throw new ArgumentNullException(nameof(defaultText));
			this.filename = filename ?? throw new ArgumentNullException(nameof(filename));
		}
	}
}
