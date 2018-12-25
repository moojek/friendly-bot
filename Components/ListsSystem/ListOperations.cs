using System;
using System.Collections.Generic;
using System.IO;
using FriendlyBot.Utils;
using Newtonsoft.Json;

namespace FriendlyBot.Components.ListsSystem
{
    public static class ListOperations
    {        
        private static List<Tuple<string, string>> list;

        private static void LoadList(string listName)
        {
            list = ListsOperations.GetList(listName);
        }
        private static void SaveList(string listName)
        {
            ListsOperations.UpdateList(listName, ref list);
        }
        public static void AddItem(string listName, string value)
        {
            AddItem(listName, value, Strings.GetString("NO_NOTE"));
        }
        public static void AddItem(string listName, string value, int pos)
        {
            AddItem(listName, value, pos, Strings.GetString("NO_NOTE"));
        }
        public static void AddItem(string listName, string value, string note)
        {
            LoadList(listName);
            list.Insert(list.Count, new Tuple<string, string>(value, note));
            SaveList(listName);
        }
        public static void AddItem(string listName, string value, int pos, string note)
        {
            LoadList(listName);
            list.Insert(pos, new Tuple<string, string>(value, note));
            SaveList(listName);
        }
        public static void RemoveItem(string listName, string value)
        {
            LoadList(listName);
            var pos = list.FindIndex(i => i.Item1 == value);
            if (pos > 0)
                RemoveItem(listName, pos);
            SaveList(listName);
        }
        public static void RemoveItem(string listName, int pos)
        {
            LoadList(listName);
            list.RemoveAt(pos);
            SaveList(listName);
        }
        public static int GetItemPosition(string listName, string value)
        {
            LoadList(listName);
            return list.FindIndex(i => i.Item1.ToString() == value) + 1;
        }

        public static List<Tuple<string, string>> GetList(string listName)
        {
            LoadList(listName);
            return list;
        }
        public static void ClearList(string listName)
        {
            LoadList(listName);
            list.Clear();
            SaveList(listName);
        }
    }
}