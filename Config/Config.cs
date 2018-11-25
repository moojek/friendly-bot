using System.IO;
using FriendlyBot.Utils;
using Newtonsoft.Json;

namespace FriendlyBot.Config
{
    public static class Config
    {
        private static string configFile = FilePaths.GetFilePath("CONFIG");
        public static BotConfig botConfig;

        static Config()
        {
            if(!File.Exists(configFile))
            {
                botConfig = new BotConfig();
                string json = JsonConvert.SerializeObject(botConfig, Formatting.Indented);
                File.WriteAllText(configFile, json);
            }
            else{
                string json = File.ReadAllText(configFile);
                botConfig = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }
    }

    public struct BotConfig
    {
        public string token;
        public string prefix;
    };
}