using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class ModalEventController
    {
        public static async Task ModalEventHandler(DiscordClient sender, ModalSubmitEventArgs args)
        {
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
                        break;
                }
            }
        }
    }
}
