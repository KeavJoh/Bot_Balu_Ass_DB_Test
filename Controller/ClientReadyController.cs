using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot_Balu_Ass_DB.Data.Database;
using Bot_Balu_Ass_DB.BotSettingsModels;
using Microsoft.VisualBasic;
using DSharpPlus.SlashCommands;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class ClientReadyController
    {
        private readonly BotConfig _botConfig;
        private readonly ApplicationDbContext _context;

        public ClientReadyController(BotConfig botConfig, ApplicationDbContext context)
        {
            _botConfig = botConfig;
            _context = context;
        }
        public async Task ClientReadyHandler(DiscordClient sender, ReadyEventArgs args)
        {
            await MainMessageController.ExecutiveMainMessage(sender, _botConfig);
            await MainMessageController.ParentsMainMessage();
            await MainMessageController.DeregistrationInformationDateTimeNowMainMessage();
            await MainMessageController.DeregistrationInformationFutureMainMessage();
        }
    }
}
