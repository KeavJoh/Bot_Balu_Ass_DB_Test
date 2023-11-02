using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Model;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class ExecutiveModalController : ApplicationCommandModule
    {
        public static async Task AddChildToDBModal(ComponentInteractionCreateEventArgs args)
        {
            var modal = new DiscordInteractionResponseBuilder()
                .WithTitle("Kind hinzufügen")
                .WithCustomId("addChildToDb")
                .AddComponents(new TextInputComponent(label: "Name", "nameOfChild", "Vorname des Kindes"))
                .AddComponents(new TextInputComponent(label: "Mutter", "nameOfMother", "Name der Mutter"))
                .AddComponents(new TextInputComponent(label: "Vater", "nameOfFather", "Name des Vaters"));

            await args.Interaction.CreateResponseAsync(InteractionResponseType.Modal, modal);
        }

        public static async Task DeleteChildFromDbModal(ComponentInteractionCreateEventArgs args)
        {
            var options = await Task.Run(GlobalDataStore.GetChildList);

            var dropdown = new DiscordSelectComponent("deleteChildFromDb", "Kind zum löschen wählen", options);

            var message = new DiscordInteractionResponseBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.DarkRed)
                .WithTitle("Welches Kind soll aus der Liste entfernt werden?"))
                .AddComponents(dropdown);

            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, message);
        }
    }
}
