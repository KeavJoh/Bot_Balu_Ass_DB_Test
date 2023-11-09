using Bot_Balu_Ass_DB.BotSettingsModels;
using DSharpPlus.Entities;
using DSharpPlus;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class HelpFunctionController
    {
        public static async Task DeleteLastMessageFromChannelHelper(DiscordClient client, ModalSubmitEventArgs args)
        {
            var channelId = await client.GetChannelAsync(args.Interaction.ChannelId);
            await DeleteLastMessageInSelectedChannel(channelId);
        }

        public static async Task DeleteLastMessageFromChannelHelper(DiscordClient client, ComponentInteractionCreateEventArgs args)
        {
            var channelId = await client.GetChannelAsync(args.Interaction.ChannelId);
            await DeleteLastMessageInSelectedChannel(channelId);
        }

        //Delete all messages from given channel
        public static async Task DeleteAllMessagesFromChannelHelper(DiscordChannel discordChannel)
        {
            var allMessagesInChannel = await discordChannel.GetMessagesAsync();
            if (allMessagesInChannel.Count > 0)
            {
                await discordChannel.DeleteMessagesAsync(allMessagesInChannel);
            }
        }

        private static async Task DeleteLastMessageInSelectedChannel(DiscordChannel channelId)
        {
            var messages = await channelId.GetMessagesAsync(1);
            var latestMessage = messages.FirstOrDefault();
            await latestMessage.DeleteAsync();
        }

        //Parse strinf DateTime to DateTime
        public static DateTime ParseStringToDateTimeHelper(string stringDateTime)
        {
            DateTime finalDateTime;
            string stringFormat = "dd.MM.yyyy";
            try
            {
                finalDateTime = DateTime.ParseExact(stringDateTime, stringFormat, CultureInfo.InvariantCulture);
            }
            catch(FormatException)
            {
                finalDateTime = DateTime.MinValue;
            }
            return finalDateTime;
        }
    }
}
