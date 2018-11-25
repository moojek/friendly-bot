using System;
using System.Collections.Generic;
using System.IO;
using FriendlyBot.Utils;
using Newtonsoft.Json;

namespace FriendlyBot.Components.WaitingListSystem
{
    public static class WaitingList
    {
        private static List<string> people;
        private static string peopleFile = FilePaths.GetFilePath("PEOPLE");

        static WaitingList()
        {
            if (!File.Exists(peopleFile))
            {
                people = new List<string>();
                string json = JsonConvert.SerializeObject(people, Formatting.Indented);
                File.WriteAllText(peopleFile, json);
            }
            else
            {
                string json = File.ReadAllText(peopleFile);
                people = JsonConvert.DeserializeObject<List<string>>(json);
            }
        }

        public static void SavePeople()
        {
            string json = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText(peopleFile, json);
        }
        public static void AddPerson(string name)
        {
            people.Add(name);
            SavePeople();
        }
        public static void AddPerson(string name, uint pos)
        {
            people.Insert((int)pos, name);
            SavePeople();
        }
        public static void RemovePerson(string name)
        {
            people.Remove(name);
            SavePeople();
        }
        public static void RemovePerson(uint pos)
        {
            people.RemoveAt((int)pos);
            SavePeople();
        }
        public static int GetPersonPosition(string name)
        {
            return people.FindIndex(i => i == name);
        }
    }
}