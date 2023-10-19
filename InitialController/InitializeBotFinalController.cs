using Bot_Balu_Ass_DB.BotSettingsModels;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.InitialController
{
    internal class InitializeBotFinalController
    {
        public DiscordConfiguration InitializeBotFinalHandler(BotConfig botConfig)
        {
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = botConfig.botSettings.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            return discordConfig;
        }
    }
}
