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
                        if(args.Interaction.ChannelId == GlobalSettings.BotConfig.ChannelSettings.ParentsCommandChannel)
                        {
                            await ParentsActionController.AddDeregistrationChildToDbAction(args);
                        }
                        else
                        {
                            await EmployeesActionController.AddDeregistrationChildToDbAction(args);
                        }
                        await HelpFunctionController.DeleteLastMessageFromChannelHelper(client, args);
                        break;

                    case "registerChildPerformedFromParentModal":
                        if(args.Interaction.ChannelId == GlobalSettings.BotConfig.ChannelSettings.ParentsCommandChannel)
                        {
                            await ParentsActionController.CompleteRegistrationToDbAction(args);
                        }
                        else
                        {
                            await EmployeesActionController.CompleteRegistrationToDbAction(args);
                        }
                        await HelpFunctionController.DeleteLastMessageFromChannelHelper(client, args);
                        break;
                    case "addChildToDb":
                        await ExecutiveActionController.AddChildToDbAction(args);
                        break;
                }
            }
        }
    }
}
