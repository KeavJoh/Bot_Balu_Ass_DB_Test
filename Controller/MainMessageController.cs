using Bot_Balu_Ass_DB.BotSettingsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class MainMessageController
    {
        public static async Task ExecutiveMainMessage(DiscordClient sender, BotConfig botConfig)
        {
            var chanelId = await sender.GetChannelAsync(botConfig.ChannelSettings.ExecutiveCommandChannel);
            await DeleteAllMessagesFromChannel(chanelId);

            DiscordButtonComponent addChildToListButton = new DiscordButtonComponent(ButtonStyle.Primary, "addChildToListButton", "Kind hinzufügen");
            DiscordButtonComponent deleteChildFromListButton = new DiscordButtonComponent(ButtonStyle.Danger, "deleteChildFromListButton", "Kind entfernen");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.DarkBlue)
                .WithTitle("Hallo und herzlich Willkommen")
                .WithDescription("Hier kann der Vorstand verschiedene Befehle ausführen. Klicke dazu einfach auf den gewünschten Befehl unter dieser Nachricht."))
                .AddComponents(addChildToListButton)
                .AddComponents(deleteChildFromListButton);

            await chanelId.SendMessageAsync(message);
        }

        private static async Task DeleteAllMessagesFromChannel(DiscordChannel discordChannel)
        {
            var allMessagesInChannel = await discordChannel.GetMessagesAsync();
            if (allMessagesInChannel.Count > 0)
            {
                await discordChannel.DeleteMessagesAsync(allMessagesInChannel);
            }
        }
    }
}
