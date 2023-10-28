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
        public static async Task ButtonEventHandler(DiscordClient sender, ComponentInteractionCreateEventArgs args)
        {
            var buttonId = args.Interaction.Data.CustomId;

            switch (buttonId)
            {
                case "deregister":
                    break;
                case "register":
                    break;
                case "addChildToListButton":
                    await ExecutiveModalController.AddChildToDB(args);
                    break;
                case "deleteChildFromListButton":
                    break;
            }
        }
    }
}
