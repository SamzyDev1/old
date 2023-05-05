using System;
using System.IO;
using System.Threading.Tasks;
using JNogueira.Discord.Webhook.Client;

namespace FrameV2
{
    public class Discord
    {
        private static DiscordWebhookClient pcclient = new DiscordWebhookClient(Config.webhook);
        public static async void SendMessage(string name, string b4, string s4, string profit, string buycommand, string sellcommand, string buyavg, string quantity, string openorders, string platform, string url_name, bool mod = false)
        {
            Program.pp = Program.pp + Convert.ToInt32(profit);
            if (Config.usewebhook)
            {
                var message = new DiscordMessage(
    username: "Agora",
    avatarUrl: "https://cdn.discordapp.com/attachments/1063171784389693631/1063317016498409472/IMG_3099.png",
    embeds: new[]
    {
        new DiscordMessageEmbed(
            "Snipe Found (View Orders)",
            color: 0,
            url: $"https://warframe.market/items/{url_name}",
            fields: new[]
            {
                new DiscordMessageEmbedField("Item Name", $"`{name}`"),
                new DiscordMessageEmbedField("Buy Price (Per Item)", $"`{b4}`"),
                new DiscordMessageEmbedField("Sell Price (Per Item)", $"`{s4}`"),
                new DiscordMessageEmbedField("Current Market Average", $"`{buyavg}`"),
                new DiscordMessageEmbedField("Visible Buy Orders", $"`{openorders}`"),
                new DiscordMessageEmbedField("Platinum Profit (Per Item)", $"`{profit}`"),
                new DiscordMessageEmbedField("Buy Order Volume", $"`{quantity}`"),
            },
            footer: new DiscordMessageEmbedFooter($"Jan 15th Release", "https://cdn.discordapp.com/attachments/1063171784389693631/1063317016498409472/IMG_3099.png")
        )
    }
);
                var buycommandmessage = new DiscordMessage(
        buycommand,
        username: "Agora",
        avatarUrl: "https://cdn.discordapp.com/attachments/1063171784389693631/1063317016498409472/IMG_3099.png",
        tts: false
    );
                var sellcommandmessage = new DiscordMessage(
        sellcommand,
        username: "Agora",
        avatarUrl: "https://cdn.discordapp.com/attachments/1063171784389693631/1063317016498409472/IMG_3099.png",
        tts: false
    );
                if (mod == false)
                {
                    await pcclient.SendToDiscord(message);
                    await pcclient.SendToDiscord(buycommandmessage);
                    await pcclient.SendToDiscord(sellcommandmessage);
                }
            }
            if (Config.print)
            {
                Console.WriteLine("Snipe Found");
                Console.WriteLine("Item: " + name);
                Console.WriteLine("Buy Price (Per Item): " + b4);
                Console.WriteLine("Sell Price (Per Item): " + s4);
                Console.WriteLine("Current Market Average: " + buyavg);
                Console.WriteLine("Visible Buy Orders: " + openorders);
                Console.WriteLine("Platinum Profit (Per Item): " + profit);
                Console.WriteLine("Buy Order Volume: " + quantity);
            }
        }
    }
}