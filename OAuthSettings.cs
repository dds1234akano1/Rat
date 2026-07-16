using System.IO;
using Newtonsoft.Json;

namespace Doug_s_dc_tool
{
    public static class OAuthSettings
    {
        public static string AccessToken = "";

        public static void Load()
        {
            if (!File.Exists("oauth_settings.json"))
            {
                Save();
                return;
            }

            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText("oauth_settings.json"));
            AccessToken = json.AccessToken;
        }

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(new
            {
                AccessToken = AccessToken
            }, Formatting.Indented);

            File.WriteAllText("oauth_settings.json", json);
        }
    }
}
