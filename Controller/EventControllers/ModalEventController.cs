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
using Bot_Balu_Ass_DB.Controller.ActionControllers;

namespace Bot_Balu_Ass_DB.Controller.EventControllers
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
                    case "deregisterChildPerformeFromParentModal":
                        await ParentsActionController.AddDeregistrationChildToDbAction(args);
                        await HelpFunctionController.DeleteLastMessageFromChannelHelper(client, 2);
                        break;
                    case "registerChildPerformedFromParentModal":
                        await ParentsActionController.CompleteRegistrationToDbAction(args);
                        await HelpFunctionController.DeleteLastMessageFromChannelHelper(client, 2);
                        break;
                    case "addChildToDb":
                        await ExecutiveActionController.AddChildToDbAction(args);
                        break;
                }
            }
        }
    }
}
