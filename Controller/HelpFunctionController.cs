using Bot_Balu_Ass_DB.BotSettingsModels;
using DSharpPlus.Entities;
using DSharpPlus;
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
                    var chanelId = await client.GetChannelAsync(GlobalSettings.BotConfig.ChannelSettings.ExecutiveCommandChannel);
                    await DeleteLastMessageInSelectedChannel(chanelId);
                    break;
            }
        }

        private static async Task DeleteLastMessageInSelectedChannel(DiscordChannel chanelId)
        {
            var messages = await chanelId.GetMessagesAsync(1);
            var latestMessage = messages.FirstOrDefault();
            await latestMessage.DeleteAsync();
        }
    }
}
