﻿using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class SelectComponentEventController
    {
        public static async Task SelectComponentEventHandler(DiscordClient client, ComponentInteractionCreateEventArgs args)
        {
            var selectId = args.Interaction.Data.CustomId;
            switch (selectId)
            {
                case "choose_dropdown":
                    await ParentsModalController.DeregisterChild(args);
                    break;
                case "deleteChildFromDb":
                    await ExecutiveActionController.DeleteChildFromDb(args);
                    break;
            }
        }
    }
}