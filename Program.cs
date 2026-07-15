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
// To Do Reoranize Code And Classes Make It Cleaner For me/ Add Kill Yourself Button For Larp

namespace Doug_s_dc_tool
{
    internal class Program
    {

        static DiscordRPC.DiscordRpcClient rpc;

        static void StartRPC()
        {
            rpc = new DiscordRPC.DiscordRpcClient("1527048050193862748");

            rpc.Initialize();

            rpc.SetPresence(new DiscordRPC.RichPresence()
            {
                Details = "Using Rat",
                State = "Getting User Info",
                Assets = new DiscordRPC.Assets()
                {
                    LargeImageKey = "eric_train",
                    LargeImageText = "Rat Tool",
                    SmallImageKey = "image",
                    SmallImageText = "@sxriker"
                }
            });
        }





        public static AppSettings Settings = new AppSettings();

        public class AppSettings
        {
            public BannerStyle BannerStyle { get; set; } = BannerStyle.PurpleBlue;
            public ConsoleColor SolidColor { get; set; } = ConsoleColor.Cyan;
        }

        static void SaveSettings()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Settings, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("settings.json", json);
        }

        static void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                string json = File.ReadAllText("settings.json");
                Settings = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettings>(json);
            }
        }


        public enum BannerStyle
        {
            PurpleBlue,
            Rainbow,
            SolidColor,
            Randomized
        }

        public static BannerStyle CurrentBannerStyle = BannerStyle.PurpleBlue;
        public static ConsoleColor SolidBannerColor = ConsoleColor.Cyan;


        static void Main(string[] args)
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


                    case '8':
                        SettingsMenu();
                        break;


                }
            }
        }

        static void Banner()
        {
            switch (Settings.BannerStyle)
            {
                case BannerStyle.PurpleBlue:
                    BannerPurpleBlue();
                    break;

                case BannerStyle.Rainbow:
                    BannerRainbow();
                    break;

                case BannerStyle.SolidColor:
                    BannerSolidColor(Settings.SolidColor);
                    break;

                case BannerStyle.Randomized:
                    BannerRandomized();
                    break;
            }
        


        Console.ResetColor();
        }

        static void BannerPurpleBlue()
        {
            Console.WriteLine("\u001b[38;2;180;0;255m .----------------.  .----------------.  .----------------.");
            Console.WriteLine("\u001b[38;2;160;0;255m | .--------------. || .--------------. || .--------------. |");
            Console.WriteLine("\u001b[38;2;140;0;255m | |  _______     | || |      __      | || |  _________   | |");
            Console.WriteLine("\u001b[38;2;120;0;255m | | |_   __ \\    | || |     /  \\     | || | |  _   _  |  | |");
            Console.WriteLine("\u001b[38;2;100;0;255m | |   | |__) |   | || |    / /\\ \\    | || | |_/ | | \\_|  | |");
            Console.WriteLine("\u001b[38;2;80;0;255m | |   |  __ /    | || |   / ____ \\   | || |     | |      | |");
            Console.WriteLine("\u001b[38;2;60;0;255m | |  _| |  \\ \\_  | || | _/ /    \\ \\_ | || |    _| |_     | |");
            Console.WriteLine("\u001b[38;2;40;0;255m | | |____| |___| | || ||____|  |____|| || |   |_____|    | |");
            Console.WriteLine("\u001b[38;2;20;0;255m | |              | || |              | || |              | |");
            Console.WriteLine("\u001b[38;2;0;0;255m  '--------------' || '--------------' || '--------------' ");
            Console.WriteLine("\u001b[38;2;0;40;255m  '----------------'  '----------------'  '----------------' ");
            Console.WriteLine("\u001b[38;2;0;80;255m                    Rat - @sxriker @larppppppppppppppppp");
            Console.WriteLine("\u001b[0m");
        }

        static void BannerRainbow()
        {
            string[] colors =
            {
        "\u001b[38;2;255;0;0m",
        "\u001b[38;2;255;127;0m",
        "\u001b[38;2;255;255;0m",
        "\u001b[38;2;0;255;0m",
        "\u001b[38;2;0;0;255m",
        "\u001b[38;2;75;0;130m",
        "\u001b[38;2;148;0;211m"
    };

            int i = 0;

            void L(string t)
            {
                Console.WriteLine(colors[i % colors.Length] + t);
                i++;
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

        static void BannerSolidColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(" .----------------.  .----------------.  .----------------.");
            Console.WriteLine(" | .--------------. || .--------------. || .--------------. |");
            Console.WriteLine(" | |  _______     | || |      __      | || |  _________   | |");
            Console.WriteLine(" | | |_   __ \\    | || |     /  \\     | || | |  _   _  |  | |");
            Console.WriteLine(" | |   | |__) |   | || |    / /\\ \\    | || | |_/ | | \\_|  | |");
            Console.WriteLine(" | |   |  __ /    | || |   / ____ \\   | || |     | |      | |");
            Console.WriteLine(" | |  _| |  \\ \\_  | || | _/ /    \\ \\_ | || |    _| |_     | |");
            Console.WriteLine(" | | |____| |___| | || ||____|  |____|| || |   |_____|    | |");
            Console.WriteLine(" | |              | || |              | || |              | |");
            Console.WriteLine("  '--------------' || '--------------' || '--------------' ");
            Console.WriteLine("  '----------------'  '----------------'  '----------------' ");
            Console.WriteLine("                    Rat - @sxriker @larppppppppppppppppp");

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
        }

    }