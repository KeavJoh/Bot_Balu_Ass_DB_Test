using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot_Balu_Ass_DB.Data.Database;
using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.InitialController;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class ModalEventController
    {

        public static async Task ModalEventHandler(DiscordClient sender, ModalSubmitEventArgs args)
        {
            //InitializeBotConfigController botConfigController = new InitializeBotConfigController();
            //BotConfig botConfig = botConfigController.InitializeBotConfig();
            //ApplicationDbContext context = new ApplicationDbContext(botConfig);

            if (args.Interaction.Type == InteractionType.ModalSubmit)
            {
                var modalId = args.Interaction.Data.CustomId;

                switch (modalId)
                {
                    case "deregisterModal":
                        break;
                    case "registerModal":
                        break;
                    case "addChildToDb":
                        await ExecutiveActionController.AddChildToDbAction(args);
                        break;
                }
            }
        }
    }
}
