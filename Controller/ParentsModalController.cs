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
                .WithTitle("Kind abmelden")
                .WithCustomId("deregisterChildFromParent")
                .AddComponents(new TextInputComponent(label: "Von Datum (im Format tt.MM.jjjj)", "dateFrom", "bsp. 20.01.2024", min_length: 10, max_length: 10))
                .AddComponents(new TextInputComponent(label: "Bis Datum (im Format tt.MM.jjjj)", "dateTo", "Nur benötigt für Abwesenheiten länger als 1 Tag", required: false, min_length: 10, max_length: 10))
                .AddComponents(new TextInputComponent(label: "Grund", "reason", "Grund der Abwesenheit",required: false , max_length: 50));

            await args.Interaction.CreateResponseAsync(InteractionResponseType.Modal, modal);
        }
    }
}
