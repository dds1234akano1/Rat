using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doug_s_dc_tool
{


    public static class DiscordOAuth
    {
        private static string clientId = "1527088965314941111"; 
        private static string redirectUri = "http://localhost:5000/callback";
        private static string oauthCode = null;

        public static async Task Login()
        {
            StartCallbackServer();

            string url =
                "https://discord.com/oauth2/authorize" +
                "?client_id=" + clientId +
                "&redirect_uri=" + Uri.EscapeDataString(redirectUri) +
                "&response_type=code" +
                "&scope=identify%20guilds";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

            Console.WriteLine("Waiting for Discord login...");

            while (oauthCode == null)
                await Task.Delay(200);

            Console.WriteLine("Received OAuth code!");

            await ExchangeCodeForToken();
        }

        private static void StartCallbackServer()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5000/");
            listener.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    var context = listener.GetContext();
                    if (context.Request.Url.AbsolutePath == "/callback")
                    {
                        oauthCode = context.Request.QueryString["code"];
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Login successful! You can close this window.");
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                    }
                }
            });
        }

        private static async Task ExchangeCodeForToken()
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "client_id", clientId },
                    { "client_secret", OAuthConfig.ClientSecret },
                    { "grant_type", "authorization_code" },
                    { "code", oauthCode },
                    { "redirect_uri", redirectUri }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://discord.com/api/oauth2/token", content);
                var json = await response.Content.ReadAsStringAsync();

                dynamic tokenData = JsonConvert.DeserializeObject(json);
                string accessToken = tokenData.access_token;

                Console.WriteLine("Access Token Received!");

                OAuthSettings.AccessToken = accessToken;
                OAuthSettings.Save();


                Console.WriteLine("Saved access token to settings.json");
            }
        }
    }
}
