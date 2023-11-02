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

        public static async Task ModalEventHandler(DiscordClient client, ModalSubmitEventArgs args)
        {
            if (args.Interaction.Type == InteractionType.ModalSubmit)
            {
                var modalId = args.Interaction.Data.CustomId;

                switch (modalId)
                {
                    case "deregisterChildPerformeFromParent":
                        await ParentsActionController.AddDeregistrationChildToDbAction(args);
                        await HelpFunctionController.DeleteLastMessageFromChannelHelper(client, 2);
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
