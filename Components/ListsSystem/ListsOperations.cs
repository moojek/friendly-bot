using System;
using System.Collections.Generic;
using System.IO;
using FriendlyBot.Utils;
using Newtonsoft.Json;

namespace FriendlyBot.Components.ListsSystem
{
    public class ListsOperations
    {
        // private static List<List<Tuple<string, string>>> lists;
        private static Dictionary<string, List<Tuple<string, string>>> lists;
        private static string listsFile = FilePaths.GetFilePath("LISTS");

        static ListsOperations()
        {
            if (!File.Exists(listsFile))
            {
                lists = new Dictionary<string, List<Tuple<string, string>>>();
                string json = JsonConvert.SerializeObject(lists, Formatting.Indented);
                File.WriteAllText(listsFile, json);
            }
            else
            {
                string json = File.ReadAllText(listsFile);
                lists = JsonConvert.DeserializeObject<Dictionary<string, List<Tuple<string, string>>>>(json);
            }
        }

        private static void SaveLists()
        {
            string json = JsonConvert.SerializeObject(lists, Formatting.Indented);
            File.WriteAllText(listsFile, json);
        }
        public static void UpdateList(string listName, ref List<Tuple<string, string>> updatedList)
        {
            lists[listName] = updatedList;
            SaveLists();
        }
        public static void AddList(string listName)
        {
            lists.Add(listName, new List<Tuple<string, string>>());
            SaveLists();
        }
        public static void RemoveList(string listName)
        {
            lists.Remove(listName);
            SaveLists();
        }
        public static List<Tuple<string, string>> GetList(string listName)
        {
            return lists[listName];
        }
        public static Dictionary<string, List<Tuple<string, string>>> GetLists()
        {
            return lists;
        }
    }
}