using System.IO;
using Newtonsoft.Json;

namespace FriendlyBot.Config
{
    public class Config
    {
        private const string configFile = "Config/Keys/config.json";
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