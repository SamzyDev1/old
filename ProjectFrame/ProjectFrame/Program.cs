using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace FrameV2
{
    internal class Program
    {
        public static List<string> UsedSnipes = new List<string>();
        public static int pp { get; set; }
        static void Main(string[] args)
        {
            Menu();
            try
            {
                Console.Clear();
                int removed = 0;
                JSON.items = Helper.GetAllItems();
                List<string> blacklist = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("./data/blacklisted.json"));
                foreach (var item in JSON.items.payload.items.ToList())
                {
                    if (blacklist.Contains(item.url_name))
                    {
                        removed++;
                        JSON.items.payload.items.Remove(item);
                    }
                }
                Console.WriteLine($"[DEBUG] Removed {removed} blacklisted items.");
            }
            catch
            {
                Console.WriteLine("Please make sure you run GetBlacklist.exe first!");
                Console.ReadKey();
                return;
            }
            Thread thread1 = new Thread(Helper.Update);
            Thread thread2 = new Thread(Helper.Update2);
            while (true)
            {
                Console.Title = $"Framey Found: {UsedSnipes.Count} Profit: {pp} Reading: {Helper.Update1Current} & {Helper.Update2Current}";
                if (thread1.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    thread1.Start();
                }
                if (thread2.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    thread2.Start();
                }
            }
        }
        private static void Menu()
        {
            Console.Title = "Project Framey | Menu";
            Console.WriteLine("Use Webhook? (Y/N)");
            string usewebhook = Console.ReadLine();
            switch (usewebhook)
            {
                case "Y":
                    Config.usewebhook= true;
                    break;
                case "N":
                    Config.usewebhook= false;
                    break;
                default:
                    Console.WriteLine("Please pick \"Y\" or \"N\"");
                    Menu();
                    break;
            }
            if (Config.usewebhook) 
            {
                Console.WriteLine("Webhook: ");
                string webhook = Console.ReadLine();
                Config.webhook = webhook;
            }
            Console.WriteLine("Print Snipes? (Y/N)");
            string print = Console.ReadLine();
            switch (print)
            {
                case "Y":
                    Config.print = true;
                    break;
                case "N":
                    Config.print = false;
                    break;
                default:
                    Console.WriteLine("Please pick \"Y\" or \"N\"");
                    Menu();
                    break;
            }
            Console.WriteLine("Platform? (PC/PS4/XBOX/SWITCH)");
            string platform = Console.ReadLine();
            switch (platform)
            {
                case "PC":
                    Config.platform = "PC";
                    return;
                case "PS4":
                    Config.platform = "PS4";
                    return;
                case "XBOX":
                    Config.platform = "XBOX";
                    return;
                case "SWITCH":
                    Config.platform = "SWITCH";
                    return;
                default:
                    Console.WriteLine("Please pick one of the options!");
                    Menu();
                    return;
            }
        }
    }
}