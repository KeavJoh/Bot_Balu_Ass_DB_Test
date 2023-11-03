using Bot_Balu_Ass_DB.BotSettingsModels;
using DSharpPlus.Entities;
using DSharpPlus;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class HelpFunctionController
    {
        public static async Task DeleteLastMessageFromChannelHelper(DiscordClient client, int caseInt)
        {
            switch (caseInt)
            {
                case 1:
                    var chanelId1 = await client.GetChannelAsync(GlobalSettings.BotConfig.ChannelSettings.ExecutiveCommandChannel);
                    await DeleteLastMessageInSelectedChannel(chanelId1);
                    break;
                case 2:
                    var chanelId2 = await client.GetChannelAsync(GlobalSettings.BotConfig.ChannelSettings.ParentsCommandChannel);
                    await DeleteLastMessageInSelectedChannel(chanelId2);
                    break;
            }
        }

        private static async Task DeleteLastMessageInSelectedChannel(DiscordChannel chanelId)
        {
            var messages = await chanelId.GetMessagesAsync(1);
            var latestMessage = messages.FirstOrDefault();
            await latestMessage.DeleteAsync();
        }

        public static DateTime ParseStringToDateTime(string stringDateTime)
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

        //Delete all messages from given channel
        public static async Task DeleteAllMessagesFromChannel(DiscordChannel discordChannel)
        {
            var allMessagesInChannel = await discordChannel.GetMessagesAsync();
            if (allMessagesInChannel.Count > 0)
            {
                await discordChannel.DeleteMessagesAsync(allMessagesInChannel);
            }
        }
    }
}
