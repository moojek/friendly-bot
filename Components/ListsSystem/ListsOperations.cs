using System;
using System.Collections.Generic;
using System.IO;
using FriendlyBot.Utils;
using Newtonsoft.Json;

namespace FriendlyBot.Components.ListsSystem
{
    public class ListsOperations
    {
        private static List<List<Tuple<string, string>>> lists;
        private static string listsFile = FilePaths.GetFilePath("LISTS");

        static ListsOperations()
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
        public static void UpdateList(int listNum, ref List<Tuple<string,string>> updatedList)
        {
            lists[listNum] = updatedList;
            SaveLists();
        }
        public static void AddList()
        {
            lists.Add(new List<Tuple<string, string>>());
            SaveLists();
        }
        public static void RemoveList(int listNum)
        {
            lists.RemoveAt(listNum);
            SaveLists();
        }
        public static List<Tuple<string, string>> GetList(int listNum)
        {
            return lists[listNum];
        }
        public static List<List<Tuple<string, string>>> GetLists()
        {
            return lists;
        }
    }
}