using System;
using System.Collections.Generic;
using System.IO;
using FriendlyBot.Utils;
using Newtonsoft.Json;

namespace FriendlyBot.Components.ListsSystem
{
    public static class ListOperations
    {
        // private static List<KeyValuePair<string, string>> people;
        private static List<List<Tuple<string, string>>> lists;
        private static string listsFile = FilePaths.GetFilePath("LISTS");

        static ListOperations()
        {
            if (!File.Exists(listsFile))
            {
                lists = new List<List<Tuple<string, string>>>();
                string json = JsonConvert.SerializeObject(lists, Formatting.Indented);
                File.WriteAllText(listsFile, json);
            }
            else
            {
                string json = File.ReadAllText(listsFile);
                lists = JsonConvert.DeserializeObject<List<List<Tuple<string, string>>>>(json);
            }
        }

        public static void SaveLists()
        {
            string json = JsonConvert.SerializeObject(lists, Formatting.Indented);
            File.WriteAllText(listsFile, json);
        }
        public static void AddItem(uint listNum, string name)
        {
            AddItem(listNum, name, (uint)lists.Count, Strings.GetString("NO_NOTE"));
        }
        public static void AddItem(uint listNum, string name, uint pos)
        {
            AddItem(listNum, name, pos, Strings.GetString("NO_NOTE"));
        }
        public static void AddItem(uint listNum, string name, string note)
        {
            AddItem(listNum, name, (uint)lists.Count, note);
        }
        public static void AddItem(uint listNum, string name, uint pos, string note)
        {
            lists[(int)listNum].Insert((int)pos, new Tuple<string, string>(name, note));
            SaveLists();
        }
        public static void RemovePerson(uint listNum, string name)
        {
            var pos = lists[(int)listNum].FindIndex(i => i.Item1 == name);
            if (pos > 0)
                RemovePerson(listNum, (uint)pos);
        }
        public static void RemovePerson(uint listNum, uint pos)
        {
            lists[(int)listNum].RemoveAt((int)pos);
            SaveLists();
        }
        public static int GetPersonPosition(uint listNum, string name)
        {
            return lists[(int)listNum].FindIndex(i => i.Item1.ToString() == name) + 1;
        }

        public static List<Tuple<string, string>> GetList(uint listNum)
        {
            return lists[(int)listNum];
        }

        public static void ClearList(uint listNum)
        {
            lists[(int)listNum].Clear();
            SaveLists();
        }
    }
}