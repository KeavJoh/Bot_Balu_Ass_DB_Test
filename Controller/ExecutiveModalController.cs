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
        public static async Task AddChildToDB(ComponentInteractionCreateEventArgs args)
        {
            var modal = new DiscordInteractionResponseBuilder()
                .WithTitle("Kind hinzufügen")
                .WithCustomId("addChildToDb")
                .AddComponents(new TextInputComponent(label: "Name", "nameOfChild", "Vorname des Kindes"));

            await args.Interaction.CreateResponseAsync(InteractionResponseType.Modal, modal);
        }

        public static async Task DeleteChildFromDb(ComponentInteractionCreateEventArgs args)
        {
            List<ChildModel> childsFromDb = GlobalDataStore.ChildList;

            var options = new List<DiscordSelectComponentOption>();

            foreach (var child in childsFromDb)
            {
                options.Add(new DiscordSelectComponentOption(child.Name, child.Id.ToString()));
            }

            var dropdown = new DiscordSelectComponent("deleteChildFromDb", "Kind zum löschen wählen", options);

            var message = new DiscordInteractionResponseBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.DarkRed)
                .WithTitle("Welches Kind soll aus der Liste entfernt werden?"))
                .AddComponents(dropdown);

            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, message);
        }
    }
}
