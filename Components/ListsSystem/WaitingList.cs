using System;
using System.Collections.Generic;
using System.IO;
using FriendlyBot.Utils;
using Newtonsoft.Json;

namespace FriendlyBot.Components.ListsSystem
{
    public static class WaitingList
    {
        private static List<KeyValuePair<string, string>> people;
        private static string peopleFile = FilePaths.GetFilePath("WAITING");

        static WaitingList()
        {
            if (!File.Exists(peopleFile))
            {
                people = new List<KeyValuePair<string, string>>();
                string json = JsonConvert.SerializeObject(people, Formatting.Indented);
                File.WriteAllText(peopleFile, json);
            }
            else
            {
                string json = File.ReadAllText(peopleFile);
                people = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(json);
            }
        }

        public static void SavePeople()
        {
            string json = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText(peopleFile, json);
        }
        public static void AddPerson(string name)
        {
            AddPerson(name, (uint)people.Count, Strings.GetString("NO_NOTE"));
        }
        public static void AddPerson(string name, uint pos)
        {
            AddPerson(name, pos, Strings.GetString("NO_NOTE"));
        }
        public static void AddPerson(string name, string note)
        {
            AddPerson(name, (uint)people.Count, note);
        }
        public static void AddPerson(string name, uint pos, string note)
        {
            people.Insert((int)pos, new KeyValuePair<string, string>(name, note));
            SavePeople();
        }
        public static void RemovePerson(string name)
        {
            var pos = people.FindIndex(i => i.Key == name);
            RemovePerson((uint)pos);
        }
        public static void RemovePerson(uint pos)
        {
            people.RemoveAt((int)pos);
            SavePeople();
        }
        public static int GetPersonPosition(string name)
        {
            return people.FindIndex(i => i.Key.ToString() == name) + 1;
        }

        public static List<KeyValuePair<string, string>> GetList()
        {
            return people;
        }
    }
}