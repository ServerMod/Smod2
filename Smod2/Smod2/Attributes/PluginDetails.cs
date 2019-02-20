using System;

namespace Smod2.Attributes
{
	public class PluginDetails : Attribute
	{
		public int SmodMajor;
		public int SmodMinor;
		public int SmodRevision;
		public string id;
		public string name;
		public string author;
		public string description;
		public string configPrefix;
		public string langFile;
		public string version;
	}
}
