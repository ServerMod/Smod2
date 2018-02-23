using System;

namespace Smod2.Attributes
{
    public class PluginDetails : Attribute
    {
		public int SmodMajor;
		public int SmodMinor;
		public int SmodRevision;
		public string id;
		public String name;
		public String author;
		public String description;
		public String version;
    }
}
