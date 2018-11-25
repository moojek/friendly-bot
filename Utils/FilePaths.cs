using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FriendlyBot.Utils
{
    public static class FilePaths
    {
        private static Dictionary<string, string> filePaths;
        private static string filePathsFile = "Config/Keys/filepaths.json";

        static FilePaths()
        {
            if (!File.Exists(filePathsFile))
            {
                filePaths = new Dictionary<string, string>();
                string json = JsonConvert.SerializeObject(filePaths, Formatting.Indented);
                File.WriteAllText(filePathsFile, json);
            }
            else
            {
                string json = File.ReadAllText(filePathsFile);
                filePaths = JsonConvert.DeserializeObject<dynamic>(json);
            }
        }

        public static string GetFilePath(string key)
        {
            if (filePaths.ContainsKey(key))
                return filePaths[key];
            return "";
        }
    }
}