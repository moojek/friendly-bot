using System.Collections.Generic;
using System.IO;
using FriendlyBot.Utils;
using Newtonsoft.Json;

namespace FriendlyBot.Components.ListsSystem
{
    public static class DetermineLists
    {
        private static Dictionary<string, int> listsDict;
        private static string listsDictFile = FilePaths.GetFilePath("LISTS_DICT");
        static DetermineLists()
        {
            if (!File.Exists(listsDictFile))
            {
                listsDict = new Dictionary<string, int>();
                string json = JsonConvert.SerializeObject(listsDict, Formatting.Indented);
                File.WriteAllText(listsDictFile, json);
            }
            else
            {
                string json = File.ReadAllText(listsDictFile);
                listsDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            }
        }
        public static int DetermineList(string list)
        {
            listsDict.TryGetValue(list, out int listNum);
            return listNum;
        }
    }
}