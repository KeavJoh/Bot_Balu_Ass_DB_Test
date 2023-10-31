using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class ParentsModalController : ApplicationCommandModule
    {
        public static async Task DeregisterChildModal(ComponentInteractionCreateEventArgs args)
        {
            var modal = new DiscordInteractionResponseBuilder()
                .WithTitle("Kind hinzufügen")
                .WithCustomId("addChildToDb")
                .AddComponents(new TextInputComponent(label: "Name", "nameOfChild", "Vorname des Kindes"));

            await args.Interaction.CreateResponseAsync(InteractionResponseType.Modal, modal);
        }
    }
}
