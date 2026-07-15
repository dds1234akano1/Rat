using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DiscordRPC;

// This Code Was Made By Doug Only Use When Given Credits Discord: @sxriker
namespace Doug_s_dc_tool
{
    internal class Program
    {  static void Main(string[] args)
        {
            Console.Title = "Rat";

            StartRPC();

            while (true)
            {
                LoadSettings();
                Console.Clear();
                Banner();
                Menu();


                ConsoleKeyInfo input = Console.ReadKey();
                char option = input.KeyChar;
                Console.WriteLine(option);
                switch (option)
                {
                    case '1':
                        webhookMessage().Wait();
                        break;

                    case '2':
                        viewUserInfo();
                        break;

                    case '3':
                        webhookSpam().Wait();
                        break;

                    case '4':
                        UsernameSniper.StartSniper(4).Wait();
                        break;

                    case '5':
                        UsernameSniper.StartSniper(5).Wait();
                        break;

                    case '6':
                        Credits();
                        break;

                        case '7':
                        rpc.Dispose();
                        return;
            }
            }
        }

        static void Banner() {            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
 .----------------.  .----------------.  .----------------. 
| .--------------. || .--------------. || .--------------. |
| |  _______     | || |      __      | || |  _________   | |
| | |_   __ \    | || |     /  \     | || | |  _   _  |  | |
| |   | |__) |   | || |    / /\ \    | || | |_/ | | \_|  | |
| |   |  __ /    | || |   / ____ \   | || |     | |      | |
| |  _| |  \ \_  | || | _/ /    \ \_ | || |    _| |_     | |
| | |____| |___| | || ||____|  |____|| || |   |_____|    | |
| |              | || |              | || |              | |
| '--------------' || '--------------' || '--------------' |
 '----------------'  '----------------'  '----------------' 
                 Rat - @sxriker @larppppppppppppppppp
");
            Console.ResetColor();
        }


        static void BannerRandomized()
        {
            Random rng = new Random();

            string RandColor()
            {
                int r = rng.Next(0, 256);
                int g = rng.Next(0, 256);
                int b = rng.Next(0, 256);
                return $"\u001b[38;2;{r};{g};{b}m";
            }

            void L(string text)
            {
                Console.WriteLine(RandColor() + text);
            }

            L(" .----------------.  .----------------.  .----------------.");
            L(" | .--------------. || .--------------. || .--------------. |");
            L(" | |  _______     | || |      __      | || |  _________   | |");
            L(" | | |_   __ \\    | || |     /  \\     | || | |  _   _  |  | |");
            L(" | |   | |__) |   | || |    / /\\ \\    | || | |_/ | | \\_|  | |");
            L(" | |   |  __ /    | || |   / ____ \\   | || |     | |      | |");
            L(" | |  _| |  \\ \\_  | || | _/ /    \\ \\_ | || |    _| |_     | |");
            L(" | | |____| |___| | || ||____|  |____|| || |   |_____|    | |");
            L(" | |              | || |              | || |              | |");
            L("  '--------------' || '--------------' || '--------------' ");
            L("  '----------------'  '----------------'  '----------------' ");
            L("                    Rat - @sxriker @larppppppppppppppppp");

            Console.WriteLine("\u001b[0m");
        } 
       static void Menu()
        {
            Console.WriteLine("\n1. Send Webhook Message");
            Console.WriteLine("2. User Info");
            Console.WriteLine("3. Webhook Spammer");
            Console.WriteLine("4. 4 Letter Steam Username Snip");
            Console.WriteLine("5. 5 Letter Steam Username Snip");
            Console.WriteLine("6. Credits");
            Console.WriteLine("7. Exit");
            Console.WriteLine("8. Settings");
        }

        static void SettingsMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Settings ===");
            Console.WriteLine("1. Purple And Blue Gradient");
            Console.WriteLine("2. Gay :3 Gradient");
            Console.WriteLine("3. Solid Color");
            Console.WriteLine("4. Back");
            Console.WriteLine("5. Randomized (RNG Colors)");


            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Settings.BannerStyle = BannerStyle.PurpleBlue;
                    SaveSettings();
                    break;

                case "2":
                    Settings.BannerStyle = BannerStyle.Rainbow;
                    SaveSettings();
                    break;

                case "3":
                    ChooseSolidColor();
                    Settings.BannerStyle = BannerStyle.SolidColor;
                    SaveSettings();
                    break;

                case "5":
                    Settings.BannerStyle = BannerStyle.Randomized;
                    SaveSettings();
                    break;


                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        static void ChooseSolidColor()
        {
            Console.Clear();
            Console.WriteLine("Choose a color:");

            foreach (var color in Enum.GetValues(typeof(ConsoleColor)))
                Console.WriteLine("- " + color);

            Console.Write("\nColor: ");
            string input = Console.ReadLine();

            if (Enum.TryParse(input, true, out ConsoleColor selected))
            {
                Settings.SolidColor = selected;
                SaveSettings();   
                Console.WriteLine("Saved!");
            }
            else
            {
                Console.WriteLine("Invalid color, keeping previous.");
            }
        }



        static async Task webhookMessage()
        {
            Console.Clear();
            Console.Write("Webhook URL: ");
            string webhook = Console.ReadLine();

            Console.Write("Message: ");
            string message = Console.ReadLine();

            Console.Write("File path (leave empty for no file): ");
            string filePath = Console.ReadLine();

            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();

                var payload = new { content = message };
                string payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                form.Add(new StringContent(payloadJson, Encoding.UTF8, "application/json"), "payload_json");

                if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    ByteArrayContent fileContent = new ByteArrayContent(fileBytes);
                    form.Add(fileContent, "file", Path.GetFileName(filePath));
                }

                HttpResponseMessage response = await client.PostAsync(webhook, form);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine("\nMessage sent successfully!");
                else
                    Console.WriteLine($"\nFailed to send message. Status code: {response.StatusCode}");
            }

            Console.WriteLine("\nReturn: 0");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                return;
        }



        static void viewUserInfo()
        {
            Console.Clear();
            Console.WriteLine("User ID: ");
            string user = Console.ReadLine();

            {
                Console.WriteLine("User Info: ");

                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://japi.rest/discord/v1/user/{user}";
                    string responseBody = client.GetStringAsync(url).Result;

                    var parsed = JObject.Parse(responseBody);
                    var data = parsed["data"];

                    string username = (string)data["username"];
                    string globalName = (string)data["global_name"];
                    string avatarUrl = (string)data["avatarURL"];
                    string bannerColor = (string)data["banner_color"];
                    string createdAt = (string)data["createdAt"];

                    Console.WriteLine("Username: " + username);
                    Console.WriteLine("Global Name: " + globalName);
                    Console.WriteLine("Avatar URL: " + avatarUrl);
                    Console.WriteLine("Banner Color: " + bannerColor);
                    Console.WriteLine("Created At: " + createdAt);


                    Console.WriteLine("Return: 0");

                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                        return;
                }
            }
        }


        static void Credits()
        {
            Console.Clear();
            Console.WriteLine("Doug");
            Console.WriteLine("Larp");
            Console.WriteLine("\nReturn: 0");

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                return;
        }

        static async Task webhookSpam()
        {
            Console.Clear();
            Console.Write("Webhook URL: ");
            string webhook = Console.ReadLine();

            Console.Write("Message: ");
            string message = Console.ReadLine();

            Console.Write("Delay between messages (ms): ");
            int delay = int.Parse(Console.ReadLine());

            Console.WriteLine("\nSpamming started! Press 'S' to stop.\n");

            using (HttpClient client = new HttpClient())
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.S)
                        {
                            Console.WriteLine("\nStopped spamming.");
                            break;
                        }
                    }

                    var payload = new { content = message };
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(webhook, content);

                    if (response.IsSuccessStatusCode)
                        Console.WriteLine("Sent!");
                    else
                        Console.WriteLine($"Failed: {response.StatusCode}");

                    await Task.Delay(delay);
                }
            }

            Console.WriteLine("\nReturn: 0");
            var exitKey = Console.ReadKey(true).Key;
            if (exitKey == ConsoleKey.D0 || exitKey == ConsoleKey.NumPad0)
                return;
        }
    }



    class UsernameSniper
    {
        static HttpClient client = new HttpClient();

        static async Task<bool> IsAvailable(string username)
        {
            var url = $"https://steamcommunity.com/id/{{username}}\";\r\n";
            var response = await client.GetAsync(url);

            return response.StatusCode == System.Net.HttpStatusCode.NotFound;
        }

        static IEnumerable<string> GenerateUsernames(int length)
        {
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            if (length == 4)
            {
                foreach (char a in alphabet)
                    foreach (char b in alphabet)
                        foreach (char c in alphabet)
                            foreach (char d in alphabet)
                                yield return $"{a}{b}{c}{d}";
            }
            else if (length == 5)
            {
                foreach (char a in alphabet)
                    foreach (char b in alphabet)
                        foreach (char c in alphabet)
                            foreach (char d in alphabet)
                                foreach (char e in alphabet)
                                    yield return $"{a}{b}{c}{d}{e}";
            }
        }

        public static async Task StartSniper(int length)
        {
            string fileName = $"{length}LetterUsernames.txt";

            Console.WriteLine($"[+] Sniping {length}-letter usernames...");
            Console.WriteLine("[+] Press ANY key to stop.\n");

            StreamWriter writer = new StreamWriter(fileName, true);

            try
            {
                foreach (var username in GenerateUsernames(length))
                {
                    if (Console.KeyAvailable)
                        break;

                    bool available = await IsAvailable(username);

                    if (available)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"AVAILABLE → {username}");

                        await writer.WriteLineAsync(username);
                        await writer.FlushAsync();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"TAKEN → {username}");
                    }
                }
            }
            finally
            {
                writer.Close();
            }

            Console.ResetColor();
            Console.WriteLine($"\n[+] Finished. Saved results to {fileName}");
        }

        class SteamSniper
        {
            static HttpClient client = new HttpClient();

            static async Task<bool> IsAvailable(string username)
            {
                string url = $"https://steamcommunity.com/id/{username}";
                var response = await client.GetAsync(url);

                return response.StatusCode == System.Net.HttpStatusCode.NotFound;
            }

            static IEnumerable<string> GenerateUsernames(int length)
            {
                char[] allowed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-.".ToCharArray();

                if (length == 4)
                {
                    foreach (char a in allowed)
                        foreach (char b in allowed)
                            foreach (char c in allowed)
                                foreach (char d in allowed)
                                    yield return $"{a}{b}{c}{d}";
                }
                else if (length == 5)
                {
                    foreach (char a in allowed)
                        foreach (char b in allowed)
                            foreach (char c in allowed)
                                foreach (char d in allowed)
                                    foreach (char e in allowed)
                                        yield return $"{a}{b}{c}{d}{e}";
                }
            }

            public static async Task StartSniper(int length)
            {
                string fileName = $"Steam_{length}Letter_Available.txt";
                StreamWriter writer = new StreamWriter(fileName, true);

                Console.WriteLine($"[+] Sniping Steam {length}-character vanity URLs...");
                Console.WriteLine("[+] Press ANY key to stop.\n");

                try
                {
                    foreach (var username in GenerateUsernames(length))
                    {
                        if (Console.KeyAvailable)
                            break;

                        bool available = await IsAvailable(username);

                        if (available)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"AVAILABLE → {username}");

                            await writer.WriteLineAsync(username);
                            await writer.FlushAsync();

                            Console.ResetColor();
                            Console.WriteLine("\nFound one! Do you want to keep going? (y/n)");

                            var key = Console.ReadKey(true).Key;

                            if (key == ConsoleKey.N)
                            {
                                Console.WriteLine("\nStopping sniper.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nContinuing...");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"TAKEN → {username}");
                        }
                    }
                }
                finally
                {
                    writer.Close();
                }

                Console.ResetColor();
                Console.WriteLine($"\n[+] Finished. Saved results to {fileName}");
            }
        }
        
