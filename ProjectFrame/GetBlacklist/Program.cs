using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GetBlacklist
{
    internal class Program
    {
        public static List<string> blacklisted = new List<string>();
        static void Main(string[] args)
        {
            Console.Title = "Item Blacklist";
            Console.WriteLine("[DEBUG] Getting Items to Blacklist");
            WebClient webby = new WebClient();
            ItemRootClassRoot localAPI = JsonConvert.DeserializeObject<ItemRootClassRoot>(webby.DownloadString($"https://api.warframe.market/v1/items"));
            foreach (var item in localAPI.payload.items)
            {
                Console.WriteLine("[DEBUG] Getting Info On " + item.item_name);
                if (ItemHasModRank(item.url_name) == 1)
                {
                    Console.WriteLine("[DEBUG] " + item.item_name + " has mod/ranks!");
                    blacklisted.Add(item.url_name);
                }
            }
            Console.WriteLine("[DEBUG] Saving!");
            File.WriteAllText("blacklisted.json", JsonConvert.SerializeObject(blacklisted));
            Console.WriteLine("[DEBUG] Done!");
            Console.ReadKey();
        }
        public static int ItemHasModRank(string item)
        {
            try
            {
                WebClient webby = new WebClient();
                var json = webby.DownloadString($"https://api.warframe.market/v1/items/{item}");
                if (json.Contains("\"mod_max_rank\""))
                {
                    if (!json.Contains("\"mod_max_rank\": 0"))
                    {
                        return 1;
                    }
                    return 0;
                }
                return 0;
            }
            catch (System.Net.WebException ex)
            {
                Console.Title = Console.Title + " (Ratelimited!)";
                Thread.Sleep(2000);
                Console.Title = Console.Title.Replace(" (Ratelimited!)", "");
                WebClient webby = new WebClient();
                var json = webby.DownloadString($"https://api.warframe.market/v1/items/{item}");
                if (json.Contains("\"mod_max_rank\""))
                {
                    if (!json.Contains("\"mod_max_rank\": 0"))
                    {
                        return 1;
                    }
                    return 0;
                }
                return 0;
            }
        }
    }
    public class ItemRootClassItem
    {
        public string item_name { get; set; }
        public string thumb { get; set; }
        public string id { get; set; }
        public string url_name { get; set; }
    }

    public class ItemRootClassPayload
    {
        public List<ItemRootClassItem> items { get; set; }
    }

    public class ItemRootClassRoot
    {
        public ItemRootClassPayload payload { get; set; }
    }
}
