using Bot_Balu_Ass_DB.BotSettingsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using Bot_Balu_Ass_DB.Data.Model;
using Microsoft.EntityFrameworkCore;

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

        public static async Task ParentsMainMessage()
        {
            var sender = GlobalSettings.DiscordClient;
            var chanelId = await sender.GetChannelAsync(GlobalSettings.BotConfig.ChannelSettings.ParentsCommandChannel);
            await DeleteAllMessagesFromChannel(chanelId);

            DiscordButtonComponent addDeregistrationButton = new DiscordButtonComponent(ButtonStyle.Primary, "addDeregistrationButton", "Kind Abmelden");
            DiscordButtonComponent deleteDeregistrationButton = new DiscordButtonComponent(ButtonStyle.Danger, "deleteDeregistrationButton", "Kind Anmelden");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.DarkBlue)
                .WithTitle("Hallo und herzlich Willkommen")
                .WithDescription("Hier kann der Vorstand verschiedene Befehle ausführen. Klicke dazu einfach auf den gewünschten Befehl unter dieser Nachricht."))
                .AddComponents(addDeregistrationButton)
                .AddComponents(deleteDeregistrationButton);

            await chanelId.SendMessageAsync(message);
        }

        public static async Task DeregistrationInformationDateTimeNowMainMessage()
        {
            var client = GlobalSettings.DiscordClient;
            var channelId = await client.GetChannelAsync(GlobalSettings.BotConfig.ChannelSettings.ParentsInformationChannel);
            await DeleteAllMessagesFromChannel(channelId);

            var deregistrationList = GlobalDataStore.DeregistrationList.Where(d => d.DeregistrationDate == DateTime.Now).ToList();

            var embedInitialMessage = new DiscordEmbedBuilder()
            {
                Title = "__Abmeldungen für heute__",
                Color = DiscordColor.Red,
                Timestamp = DateTime.Now
            };

            var descriptionBuilder = new StringBuilder();

            if (!deregistrationList.Any())
            {
                descriptionBuilder.AppendLine($"**{DateTime.Now.ToString("dd.MM.yyyy")}**");
                descriptionBuilder.AppendLine($" ");
                descriptionBuilder.AppendLine($"`Für heute sind keine Kinder abgemeldet`");

                embedInitialMessage.Description = descriptionBuilder.ToString();
            }

            await channelId.SendMessageAsync(embedInitialMessage);
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
