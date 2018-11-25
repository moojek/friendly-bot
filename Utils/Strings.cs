using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FriendlyBot.Utils
{
    public static class Strings
    {
        private static Dictionary<string, string> strings;
        private static string stringsFile = "Messages/strings.json";
        static Strings()
        {
            string json = File.ReadAllText(stringsFile);
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            strings = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetString(string key)
        {
            if (strings.ContainsKey(key))
                return strings[key];
            return "";
        }
        public static string GetString(string key, object parameter)
        {
            if (strings.ContainsKey(key))
                return GetString(strings[key], new object[] { parameter });
            return "";
        }

        public static string GetString(string key, params object[] parameter)
        {
            if (strings.ContainsKey(key))
                return String.Format(strings[key], parameter);
            return "";
        }
    }
}