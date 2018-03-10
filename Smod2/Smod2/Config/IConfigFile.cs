
namespace Smod2
{
	public interface IConfigFile
	{
		int GetIntValue(string key, int def);
		string GetStringValue(string key, string def);
		bool GetBoolValue(string key, bool def);
		string[] GetListValue(string key);
	}
}
