using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot_Balu_Ass_DB.Controller.ModalControllers;

namespace Bot_Balu_Ass_DB.Controller.EventControllers
{
    internal class ButtonEventController
    {
        public static async Task ButtonEventHandler(DiscordClient client, ComponentInteractionCreateEventArgs args)
        {
            var buttonId = args.Interaction.Data.CustomId;

            switch (buttonId)
            {
                case "addDeregistrationButton":
                    await DeregistrationRegistrationModalController.DeregisterChildDropdownModal(args);
                    break;
                case "deleteDeregistrationButton":
                    await DeregistrationRegistrationModalController.RegisterChildDropdownModal(args);
                    break;
                case "addChildToListButton":
                    await ExecutiveModalController.AddChildToDBModal(args);
                    break;
                case "deleteChildFromListButton":
                    await ExecutiveModalController.DeleteChildFromDbModal(args);
                    break;
                case "addDeregistrationForCurrontDayButton":
                    await DeregistrationRegistrationModalController.AddDeregistrationForCurrontDayDropdownModal(args);
                    break;
                case "addImportandDatesButton":
                    await ExecutiveModalController.AddImportandDateModal(args);
                    break;
            }
        }
    }
}
