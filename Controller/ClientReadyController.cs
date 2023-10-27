using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot_Balu_Ass_DB.Data.Database;
using Bot_Balu_Ass_DB.BotSettingsModels;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class ClientReadyController
    {
        private readonly ApplicationDbContext _context;
        private readonly BotConfig _botConfig;

        public ClientReadyController(ApplicationDbContext context, BotConfig botConfig)
        {
            _context = context;
            _botConfig = botConfig;
        }
        public async Task ClientReadyHandler(DiscordClient sender, ReadyEventArgs args)
        {
            await MainMessageController.ExecutiveMainMessage(sender, _botConfig);
        }
    }
}
