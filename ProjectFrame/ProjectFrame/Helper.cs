using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrameV2
{
    public class Helper
    {
        public static string Update1Current { get; set; }
        public static string Update2Current { get; set; }
        public static Root UpdateAPI(string item)
        {
            WebClient webclient = new WebClient();
            try
            {
                Root localAPI = JsonConvert.DeserializeObject<Root>(webclient.DownloadString($"https://api.warframe.market/v1/items/{item}/orders"));
                if (localAPI != null)
                { return localAPI; }
                return null;
            }
            catch
            {
                try
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(2000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    Root localAPI2 = JsonConvert.DeserializeObject<Root>(webclient.DownloadString($"https://api.warframe.market/v1/items/{item}/orders"));
                    if (localAPI2 != null)
                    { return localAPI2; }
                    return null;
                }
                catch
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(4000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    Root localAPI2 = JsonConvert.DeserializeObject<Root>(webclient.DownloadString($"https://api.warframe.market/v1/items/{item}/orders"));
                    if (localAPI2 != null)
                    { return localAPI2; }
                    return null;
                }
                
            }
        }
        public static ItemHelperRoot GetItemInfo(string item)
        {
            WebClient webclient = new WebClient();
            try
            {
                ItemHelperRoot localAPI = JsonConvert.DeserializeObject<ItemHelperRoot>(webclient.DownloadString($"https://api.warframe.market/v1/items/{item}"));
                if (localAPI != null)
                { return localAPI; }
                return null;
            }
            catch
            {
                try
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(2000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    ItemHelperRoot localAPI = JsonConvert.DeserializeObject<ItemHelperRoot>(webclient.DownloadString($"https://api.warframe.market/v1/items/{item}"));
                    if (localAPI != null)
                    { return localAPI; }
                    return null;
                }
                catch
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(4000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    ItemHelperRoot localAPI = JsonConvert.DeserializeObject<ItemHelperRoot>(webclient.DownloadString($"https://api.warframe.market/v1/items/{item}"));
                    if (localAPI != null)
                    { return localAPI; }
                    return null;
                }
            }
        }
        public static bool ItemHasModRank(string item)
        {
            WebClient webclient = new WebClient();
            try
            {
                var json = webclient.DownloadString($"https://api.warframe.market/v1/items/{item}");
                if (json.Contains("\"mod_max_rank\""))
                {
                    if (!json.Contains("\"mod_max_rank\": 0"))
                    { return true; }
                    return false;
                }
                return false;
            }
            catch
            {
                try
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(2000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    var json = webclient.DownloadString($"https://api.warframe.market/v1/items/{item}");
                    if (json.Contains("\"mod_max_rank\""))
                    {
                        if (!json.Contains("\"mod_max_rank\": 0"))
                        { return true; }
                        return false;
                    }
                    return false;
                }
                catch
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(4000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    var json = webclient.DownloadString($"https://api.warframe.market/v1/items/{item}");
                    if (json.Contains("\"mod_max_rank\""))
                    {
                        if (!json.Contains("\"mod_max_rank\": 0"))
                        { return true; }
                        return false;
                    }
                    return false;
                }
            }
        }
        public static ItemRootClassRoot GetAllItems()
        {
            WebClient webclient = new WebClient();
            try
            {
                ItemRootClassRoot localAPI = JsonConvert.DeserializeObject<ItemRootClassRoot>(webclient.DownloadString($"https://api.warframe.market/v1/items"));
                if (localAPI != null)
                { return localAPI; }
                return null;
            }
            catch
            {
                try
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(2000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    ItemRootClassRoot localAPI = JsonConvert.DeserializeObject<ItemRootClassRoot>(webclient.DownloadString($"https://api.warframe.market/v1/items"));
                    if (localAPI != null)
                    { return localAPI; }
                    return null;
                }
                catch
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(4000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    ItemRootClassRoot localAPI = JsonConvert.DeserializeObject<ItemRootClassRoot>(webclient.DownloadString($"https://api.warframe.market/v1/items"));
                    if (localAPI != null)
                    { return localAPI; }
                    return null;
                }
            }
        }
        public static List<Order> GetBuy(string item)
        {
            try
            {
                var localItemOrders = UpdateAPI(item);
                List<Order> filtered = new List<Order>();


                filtered.OrderByDescending(g => (g.platinum)); //first for highest | / g.quantity

                foreach (var order in localItemOrders.payload.orders)
                {
                    if (order.visible && order.order_type == "buy" && order.user.status == "ingame") //valid buy order
                    { filtered.Add(order); }
                }
                return filtered;
            }
            catch
            {
                try
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(2000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    var localItemOrders = UpdateAPI(item);
                    List<Order> filtered = new List<Order>();


                    filtered.OrderByDescending(g => (g.platinum)); //first for highest | / g.quantity

                    foreach (var order in localItemOrders.payload.orders)
                    {
                        if (order.visible && order.order_type == "buy" && order.user.status == "ingame") //valid buy order
                        { filtered.Add(order); }
                    }
                    return filtered;
                }
                catch
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(4000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    var localItemOrders = UpdateAPI(item);
                    List<Order> filtered = new List<Order>();


                    filtered.OrderByDescending(g => (g.platinum)); //first for highest | / g.quantity

                    foreach (var order in localItemOrders.payload.orders)
                    {
                        if (order.visible && order.order_type == "buy" && order.user.status == "ingame") //valid buy order
                        { filtered.Add(order); }
                    }
                    return filtered;
                }
            }
        }
        public static List<Order> GetSell(string item)
        {
            try
            {
                var localItemOrders = UpdateAPI(item);
                List<Order> filtered = new List<Order>();


                filtered.OrderByDescending(g => (g.platinum)); //last for lowest | I don't know if quantity matter so g.platinum/g.quantity

                foreach (var order in localItemOrders.payload.orders)
                {
                    if (order.visible && order.order_type == "sell" && order.user.status == "ingame") //valid sell order
                    {
                        filtered.Add(order);
                    }
                }
                return filtered;
            }
            catch
            {
                try
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(2000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    var localItemOrders = UpdateAPI(item);
                    List<Order> filtered = new List<Order>();


                    filtered.OrderByDescending(g => (g.platinum)); //last for lowest | I don't know if quantity matter so g.platinum/g.quantity

                    foreach (var order in localItemOrders.payload.orders)
                    {
                        if (order.visible && order.order_type == "sell" && order.user.status == "ingame") //valid sell order
                        {
                            filtered.Add(order);
                        }
                    }
                    return filtered;
                }
                catch
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(4000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    var localItemOrders = UpdateAPI(item);
                    List<Order> filtered = new List<Order>();


                    filtered.OrderByDescending(g => (g.platinum)); //last for lowest | I don't know if quantity matter so g.platinum/g.quantity

                    foreach (var order in localItemOrders.payload.orders)
                    {
                        if (order.visible && order.order_type == "sell" && order.user.status == "ingame") //valid sell order
                        {
                            filtered.Add(order);
                        }
                    }
                    return filtered;
                }
            }
        }
        public static double GetBuyAvg(string item)
        {
            try
            {
                var localItemOrders = UpdateAPI(item);
                List<int> filtered = new List<int>();
                filtered.OrderByDescending(g => (g)); //first for highest | / g.quantity
                foreach (var order in localItemOrders.payload.orders)
                {
                    if (order.visible && order.order_type == "buy" && order.user.status == "ingame") //valid buy order
                    { filtered.Add(order.platinum); }
                }
                return filtered.Average();
            }
            catch
            {
                try
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(2000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    var localItemOrders = UpdateAPI(item);
                    List<int> filtered = new List<int>();
                    filtered.OrderByDescending(g => (g)); //first for highest | / g.quantity
                    foreach (var order in localItemOrders.payload.orders)
                    {
                        if (order.visible && order.order_type == "buy" && order.user.status == "ingame") //valid buy order
                        { filtered.Add(order.platinum); }
                    }
                    return filtered.Average();
                }
                catch
                {
                    if (!Console.Title.Contains(" Ratelimited!"))
                    {
                        Console.Title = Console.Title + " Ratelimited!";
                        Thread.Sleep(4000);
                        Console.Title = Console.Title.Replace(" Ratelimited!", "");
                    }
                    var localItemOrders = UpdateAPI(item);
                    List<int> filtered = new List<int>();
                    filtered.OrderByDescending(g => (g)); //first for highest | / g.quantity
                    foreach (var order in localItemOrders.payload.orders)
                    {
                        if (order.visible && order.order_type == "buy" && order.user.status == "ingame") //valid buy order
                        { filtered.Add(order.platinum); }
                    }
                    return filtered.Average();
                }
            }
        }

        public static void Update()
        {
            List<string> toRemove = new List<string>();
            try
            {
                for (int i = JSON.items.payload.items.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        Update1Current = JSON.items.payload.items[i].item_name;
                        Order localGetSell = GetSell(JSON.items.payload.items[i].url_name).Last();
                        Order localGetBuy = GetBuy(JSON.items.payload.items[i].url_name).First();
                        if (GetSell(JSON.items.payload.items[i].url_name).Count == 0 || GetBuy(JSON.items.payload.items[i].url_name).Count == 0 || toRemove.Contains((Base64Encode(localGetSell.id + localGetBuy.id))) || JSON.items.payload.items[i].url_name == Update2Current) { continue; }
                        if ((short)(localGetBuy.platinum - localGetSell.platinum) > 0 && !Program.UsedSnipes.Contains(localGetSell.id) && !ItemHasModRank(JSON.items.payload.items[i].url_name))
                        {
                            Program.UsedSnipes.Add(localGetSell.id);
                            Discord.SendMessage(JSON.items.payload.items[i].item_name, localGetSell.platinum.ToString(), localGetBuy.platinum.ToString(), $"{localGetBuy.platinum - localGetSell.platinum}", $"/w {localGetSell.user.ingame_name} Hi! I want to buy: {JSON.items.payload.items[i].item_name} for {localGetSell.platinum} platinum. (warframe.market)", $"/w {localGetBuy.user.ingame_name} Hi! I want to sell: {JSON.items.payload.items[i].item_name} for {localGetBuy.platinum} platinum. (warframe.market)", Math.Round(GetBuyAvg(JSON.items.payload.items[i].url_name), 2).ToString(), localGetSell.quantity.ToString(), GetBuy(JSON.items.payload.items[i].url_name).Count.ToString(), localGetBuy.platform, JSON.items.payload.items[i].url_name);
                        }
                        localGetSell = null;
                        localGetBuy = null;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("contains no"))
                        {
                            continue;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write("[DEBUG] ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + ex.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static void Update2()
        {
            List<string> toRemove = new List<string>();
            try
            {
                for (int i = 0; i <= JSON.items.payload.items.Count; i++) //int i = 0; i < 5; i++
                {
                    try
                    {
                        Update2Current = JSON.items.payload.items[i].item_name;
                        Order localGetSell = GetSell(JSON.items.payload.items[i].url_name).Last();
                        Order localGetBuy = GetBuy(JSON.items.payload.items[i].url_name).First();
                        if (GetSell(JSON.items.payload.items[i].url_name).Count == 0 || GetBuy(JSON.items.payload.items[i].url_name).Count == 0 || toRemove.Contains(Base64Encode(localGetSell.id + localGetBuy.id)) || JSON.items.payload.items[i].url_name == Update1Current) { continue; }
                        if ((short)(localGetBuy.platinum - localGetSell.platinum) > 0 && !Program.UsedSnipes.Contains(localGetSell.id) && !ItemHasModRank(JSON.items.payload.items[i].url_name))
                        {
                            Program.UsedSnipes.Add(localGetSell.id);
                            Discord.SendMessage(JSON.items.payload.items[i].item_name, localGetSell.platinum.ToString(), localGetBuy.platinum.ToString(), $"{localGetBuy.platinum - localGetSell.platinum}", $"/w {localGetSell.user.ingame_name} Hi! I want to buy: {JSON.items.payload.items[i].item_name} for {localGetSell.platinum} platinum. (warframe.market)", $"/w {localGetBuy.user.ingame_name} Hi! I want to sell: {JSON.items.payload.items[i].item_name} for {localGetBuy.platinum} platinum. (warframe.market)", Math.Round(GetBuyAvg(JSON.items.payload.items[i].url_name), 2).ToString(), localGetSell.quantity.ToString(), GetBuy(JSON.items.payload.items[i].url_name).Count.ToString(), localGetBuy.platform,JSON.items.payload.items[i].url_name);
                        }
                        localGetSell = null;
                        localGetBuy = null;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("contains no"))
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("[DEBUG] ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + ex.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return;
        }
    }
}