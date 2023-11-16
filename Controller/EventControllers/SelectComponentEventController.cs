using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot_Balu_Ass_DB.BotSettingsModels;
using System.Reflection;
using System.Threading.Channels;
using Bot_Balu_Ass_DB.Controller.ModalControllers;
using Bot_Balu_Ass_DB.Controller.ActionControllers;

namespace Bot_Balu_Ass_DB.Controller.EventControllers
{
    internal class SelectComponentEventController
    {
        public static async Task SelectComponentEventHandler(DiscordClient client, ComponentInteractionCreateEventArgs args)
        {
            var selectId = args.Interaction.Data.CustomId;
            switch (selectId)
            {
                case "addDeregistrationToDbDropdown":
                    await DeregistrationRegistrationModalController.DeregisterChildModal(args);
                    break;
                case "deleteChildFromDbDropdown":
                    await HelpFunctionController.DeleteLastMessageFromChannelHelper(client, args);
                    await ExecutiveActionController.DeleteChildFromDbAction(args);
                    break;
                case "deleteDeregestrationFromDbDropdown":
                    await DeregistrationRegistrationModalController.RegistrationChildModal(args);
                    break;
                case "addDeregistrationForCurrontDayToDbDropdown":
                    await HelpFunctionController.DeleteLastMessageFromChannelHelper(client, args);
                    if (args.Interaction.ChannelId == GlobalSettings.BotConfig.ChannelSettings.ParentsCommandChannel)
                    {
                        await ParentsActionController.AddDeregistrationChildForCurrentDayToDbAction(args);
                    }
                    else
                    {
                        await EmployeesActionController.AddDeregistrationChildForCurrentDayToDbAction(args);
                    }
                    break;
            }
        }
    }
}
