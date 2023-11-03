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
using System.Globalization;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class MainMessageController
    {
        //Main message for executive in executive_command channel
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

        //Main message for parents in parents_command channel
        public static async Task ParentsMainMessage()
        {
            var sender = GlobalSettings.DiscordClient;
            var chanelId = await sender.GetChannelAsync(GlobalSettings.BotConfig.ChannelSettings.ParentsCommandChannel);
            await DeleteAllMessagesFromChannel(chanelId);

            DiscordButtonComponent addDeregistrationForCurrontDayButton = new DiscordButtonComponent(ButtonStyle.Primary, "addDeregistrationForCurrontDayButton", "Schnellabmeldung");
            DiscordButtonComponent addDeregistrationButton = new DiscordButtonComponent(ButtonStyle.Primary, "addDeregistrationButton", "Abmelden");
            DiscordButtonComponent deleteDeregistrationButton = new DiscordButtonComponent(ButtonStyle.Danger, "deleteDeregistrationButton", "Anmelden");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.DarkBlue)
                .WithTitle("Hallo und herzlich Willkommen")
                .WithDescription("Hier kannst du dein Kind Abmelden oder auch wieder Anmelden. Klicke dazu einfach auf einen der unten stehenden Befehle.")
                .AddField("Schnellabmeldung", $"`Hier kannst du dein Kind für den aktuellen Tag abmelden`")
                .AddField("Abmeldung", $"`Hier kannst du dein Kind für einen oder mehrere Tage abmelden`")
                .AddField("Anmelden", $"`Hier kannst du eine Abmeldung für einen oder mehrere Tage rückgängig machen`")
                .AddField("Heutiges Datum", $"`{DateTime.Now.Date.ToString("dd.MM.yyyy")}`", false))
                .AddComponents(addDeregistrationForCurrontDayButton)
                .AddComponents(addDeregistrationButton)
                .AddComponents(deleteDeregistrationButton);

            await chanelId.SendMessageAsync(message);
        }

        //Main information message of deregistration for actual day
        public static async Task DeregistrationInformationDateTimeNowMainMessage()
        {
            var client = GlobalSettings.DiscordClient;
            var channelId = await client.GetChannelAsync(GlobalSettings.BotConfig.ChannelSettings.ParentsInformationChannel);
            await DeleteAllMessagesFromChannel(channelId);

            var deregistrationList = GlobalDataStore.DeregistrationList.Where(d => d.DeregistrationDate == DateTime.Now.Date).ToList();

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
            }
            else
            {
                descriptionBuilder.AppendLine($"**{DateTime.Now.ToString("dd.MM.yyyy")}**");

                foreach (var deregisterChild in deregistrationList)
                {
                    descriptionBuilder.AppendLine($"`- {deregisterChild.ChildName}: {deregisterChild.Reason}`");
                }

                descriptionBuilder.AppendLine($" ");
            }

            embedInitialMessage.Description = descriptionBuilder.ToString();
            await channelId.SendMessageAsync(embedInitialMessage);
        }

        //Main information message of deregistratios in the future
        public static async Task DeregistrationInformationFutureMainMessage()
        {
            var client = GlobalSettings.DiscordClient;
            var channelId = await client.GetChannelAsync(GlobalSettings.BotConfig.ChannelSettings.ParentsInformationChannel);

            var deregistrationList = GlobalDataStore.DeregistrationList
                .Where(d => d.DeregistrationDate.Date > DateTime.Now.Date)
                .GroupBy(d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                    d.DeregistrationDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                .OrderBy(g => g.Key)
                .ToList();

            var embedInitialMessage = new DiscordEmbedBuilder()
            {
                Title = "__Zukünftige Abmeldungen__",
                Color = DiscordColor.Yellow,
                Timestamp = DateTime.Now,
            };

            var descriptionBuilder = new StringBuilder();

            if (!deregistrationList.Any())
            {
                descriptionBuilder.AppendLine($"**Es gibt keine Abmeldungen für die nächsten Tage**");
            }
            else
            {
                foreach (var weeklyGroup in deregistrationList)
                {
                    // Fügt die Kalenderwoche hinzu
                    descriptionBuilder.AppendLine($"**KW {weeklyGroup.Key}**");
                    descriptionBuilder.AppendLine("-----");

                    var dailyGroups = weeklyGroup
                        .OrderBy(d => d.DeregistrationDate)
                        .GroupBy(d => d.DeregistrationDate.Date)
                        .ToList();

                    foreach (var dailyGroup in dailyGroups)
                    {
                        string dateHeader = dailyGroup.Key.ToString("**dddd, dd.MM.yyyy**", new CultureInfo("de-DE"));
                        descriptionBuilder.AppendLine(dateHeader);

                        foreach (var deregistration in dailyGroup)
                        {
                            descriptionBuilder.AppendLine($"`- {deregistration.ChildName}: {deregistration.Reason ?? "kein Grund angegeben"}`");
                        }

                        descriptionBuilder.AppendLine(); // Fügt eine Leerzeile nach den Einträgen eines Tages hinzu
                    }

                    //descriptionBuilder.AppendLine(); // Fügt eine Leerzeile nach den Einträgen einer Woche hinzu
                }
            }

            embedInitialMessage.Description = descriptionBuilder.ToString();
            await channelId.SendMessageAsync(embed: embedInitialMessage.Build());
        }

        //Delete all messages from given channel
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
