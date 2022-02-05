
using System.Collections.Generic;

namespace Smod2
{
	public interface IConfigFile
	{
		string GetConfigPath();
		int GetIntValue(string key, int def, bool randomValues = false);
		float GetFloatValue(string key, float def, bool randomValues = false);
		string GetStringValue(string key, string def, bool randomValues = false);
		bool GetBoolValue(string key, bool def, bool randomValues = false);
		string[] GetListValue(string key, string[] def, bool randomValues = false);
		string[] GetListValue(string key, bool randomValues = false);
		int[] GetIntListValue(string key, int[] def, bool randomValues = false);
		int[] GetIntListValue(string key, bool randomValues = false);
		Dictionary<string, string> GetDictValue(string key, Dictionary<string, string> def, bool randomValues = false, char splitChar = ':');
		Dictionary<string, string> GetDictValue(string key, bool randomValues = false, char splitChar = ':');
		Dictionary<int, int> GetIntDictValue(string key, Dictionary<int, int> def, bool randomValues = false, char splitChar = ':');
		Dictionary<int, int> GetIntDictValue(string key, bool randomValues = false, char splitChar = ':');
	}
}
