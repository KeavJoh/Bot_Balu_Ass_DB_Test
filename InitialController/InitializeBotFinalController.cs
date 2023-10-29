using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Database;
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
                Token = botConfig.BotSettings.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            return discordConfig;
        }

        public Task InitializeBotGlobalSettingsHandler(BotConfig botConfig, ApplicationDbContext context, DiscordClient sender)
        {
            GlobalSettings.Context = context;
            GlobalSettings.BotConfig = botConfig;
            GlobalSettings.DiscordClient = sender;

            return Task.CompletedTask;
        }
    }
}
