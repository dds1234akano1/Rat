using System.IO;
using Newtonsoft.Json;

namespace Doug_s_dc_tool
{
    public static class OAuthConfig
    {
        public static string ClientSecret = "";

        public static void Load()
        {
            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));
            ClientSecret = json.client_secret;
        }
    }
}
