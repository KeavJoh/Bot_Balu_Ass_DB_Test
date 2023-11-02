using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class ButtonEventController
    {
        public static async Task ButtonEventHandler(DiscordClient client, ComponentInteractionCreateEventArgs args)
        {
            var buttonId = args.Interaction.Data.CustomId;

            switch (buttonId)
            {
                case "addDeregistrationButton":
                    await ParentsModalController.DeregisterChildDropdownModal(args);
                    break;
                case "deleteDeregistrationButton":
                    break;
                case "addChildToListButton":
                    await ExecutiveModalController.AddChildToDBModal(args);
                    break;
                case "deleteChildFromListButton":
                    await ExecutiveModalController.DeleteChildFromDbModal(args);
                    break;
            }
        }
    }
}
