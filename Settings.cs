using System.IO;
using Newtonsoft.Json;

namespace Doug_s_dc_tool
{
    public static class Settings
    {
        public static string AccessToken = "";

        public static void Load()
        {
            if (!File.Exists("settings.json"))
            {
                Save();
                return;
            }

            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText("settings.json"));
            AccessToken = json.AccessToken;
        }

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(new
            {
                AccessToken = AccessToken
            }, Formatting.Indented);

            File.WriteAllText("settings.json", json);
        }
    }
}
