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

namespace Bot_Balu_Ass_DB.Controller
{
    internal class SelectComponentEventController
    {
        public static async Task SelectComponentEventHandler(DiscordClient client, ComponentInteractionCreateEventArgs args)
        {
            var selectId = args.Interaction.Data.CustomId;
            switch (selectId)
            {
                case "addDeregistrationToDb":
                    await ParentsModalController.DeregisterChildModal(args);
                    break;
                case "deleteChildFromDb":
                    await ExecutiveActionController.DeleteChildFromDbAction(args);
                    await HelpFunctionController.DeleteLastMessageFromChannelHelper(client, 1);
                    break;
            }
        }
    }
}
