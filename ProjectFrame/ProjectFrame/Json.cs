using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FrameV2
{
    public class JSON 
    {
        public static ItemRootClassRoot items { get; set; }
    }
    public class Order
    {
        public bool visible { get; set; }
        public string order_type { get; set; }
        public string platform { get; set; }
        public DateTime last_update { get; set; }
        public int quantity { get; set; }
        public string region { get; set; }
        public User user { get; set; }
        public int platinum { get; set; }
        public DateTime creation_date { get; set; }
        public string subtype { get; set; }
        public string id { get; set; }
    }

    public class Payload
    {
        public List<Order> orders { get; set; }
    }

    public class Root
    {
        public Payload payload { get; set; }
    }

    public class User
    {
        public string ingame_name { get; set; }
        public DateTime last_seen { get; set; }
        public int reputation { get; set; }
        public string region { get; set; }
        public string id { get; set; }
        public string avatar { get; set; }
        public string status { get; set; }
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
    public class Cs
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class De
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class En
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class Es
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class Fr
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class ItemHelperItem
    {
        public string id { get; set; }
        public List<ItemsInSet> items_in_set { get; set; }
    }

    public class ItemsInSet
    {
        public object sub_icon { get; set; }
        public List<string> subtypes { get; set; }
        public string icon_format { get; set; }
        public string url_name { get; set; }
        public string thumb { get; set; }
        public string id { get; set; }
        public bool vaulted { get; set; }
        public string icon { get; set; }
        public List<string> tags { get; set; }
        public int trading_tax { get; set; }
        public En en { get; set; }
        public Ru ru { get; set; }
        public Ko ko { get; set; }
        public Fr fr { get; set; }
        public Sv sv { get; set; }
        public De de { get; set; }

        [JsonProperty("zh-hant")]
        public ZhHant zhhant { get; set; }

        [JsonProperty("zh-hans")]
        public ZhHans zhhans { get; set; }
        public Pt pt { get; set; }
        public Es es { get; set; }
        public Pl pl { get; set; }
        public Cs cs { get; set; }
    }

    public class Ko
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class ItemHelperPayload
    {
        public ItemHelperItem item { get; set; }
    }

    public class Pl
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class Pt
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class ItemHelperRoot
    {
        public ItemHelperPayload payload { get; set; }
    }

    public class Ru
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class Sv
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class ZhHans
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }

    public class ZhHant
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public object wiki_link { get; set; }
        public List<object> drop { get; set; }
    }
}
