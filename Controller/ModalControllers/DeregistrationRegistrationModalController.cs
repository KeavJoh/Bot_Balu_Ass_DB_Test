using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Model;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller.ModalControllers
{
    internal class DeregistrationRegistrationModalController : ApplicationCommandModule
    {
        public static async Task DeregisterChildModal(ComponentInteractionCreateEventArgs args)
        {
            var nameChild = GlobalDataStore.ChildList.FirstOrDefault(x => x.Id.ToString() == args.Values[0]).Name;

            var modal = new DiscordInteractionResponseBuilder()
                .WithTitle("Kind abmelden")
                .WithCustomId("deregisterChildPerformeFromParentModal")
                .AddComponents(new TextInputComponent(label: "Name", "nameOfChild", value: nameChild, required: true))
                .AddComponents(new TextInputComponent(label: "Von Datum (im Format tt.MM.jjjj)", "dateFrom", "bsp. 20.01.2024", min_length: 10, max_length: 10))
                .AddComponents(new TextInputComponent(label: "Bis Datum (im Format tt.MM.jjjj)", "dateTo", "Nur benötigt für Abwesenheiten länger als 1 Tag", required: false, min_length: 10, max_length: 10))
                .AddComponents(new TextInputComponent(label: "Grund", "reason", "Grund der Abwesenheit", required: false, max_length: 50));

            await args.Interaction.CreateResponseAsync(InteractionResponseType.Modal, modal);
        }

        public static async Task DeregisterChildDropdownModal(ComponentInteractionCreateEventArgs args)
        {
            var options = await Task.Run(GlobalDataStore.GetChildList);
            var dropdown = new DiscordSelectComponent("addDeregistrationToDbDropdown", "Kind zum Abmelden auswählen", options);

            var message = new DiscordInteractionResponseBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.DarkBlue)
                .WithTitle("Welches Kind soll Abgemeldet werden?"))
                .AddComponents(dropdown);

            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, message);
        }

        public static async Task RegistrationChildModal(ComponentInteractionCreateEventArgs args)
        {
            var nameChild = GlobalDataStore.ChildList.FirstOrDefault(x => x.Id.ToString() == args.Values[0]).Name;

            var modal = new DiscordInteractionResponseBuilder()
                .WithTitle("Kind Anmelden")
                .WithCustomId("registerChildPerformedFromParentModal")
                .AddComponents(new TextInputComponent(label: "Name", "nameOfChild", value: nameChild, required: true))
                .AddComponents(new TextInputComponent(label: "Von Datum (im Format tt.MM.jjjj)", "dateFrom", "bsp. 20.01.2024", min_length: 10, max_length: 10))
                .AddComponents(new TextInputComponent(label: "Bis Datum (im Format tt.MM.jjjj)", "dateTo", "Nur benötigt für Anmeldungen länger als 1 Tag", required: false, min_length: 10, max_length: 10));

            await args.Interaction.CreateResponseAsync(InteractionResponseType.Modal, modal);
        }

        public static async Task RegisterChildDropdownModal(ComponentInteractionCreateEventArgs args)
        {
            var options = await Task.Run(GlobalDataStore.GetChildList);
            var dropdown = new DiscordSelectComponent("deleteDeregestrationFromDbDropdown", "Kind zum ANmelden auswählen", options);

            var message = new DiscordInteractionResponseBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.DarkBlue)
                .WithTitle("Welches Kind soll Angemeldet werden?"))
                .AddComponents(dropdown);

            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, message);
        }

        public static async Task AddDeregistrationForCurrontDayDropdownModal(ComponentInteractionCreateEventArgs args)
        {
            var options = await Task.Run(GlobalDataStore.GetChildList);
            var dropdown = new DiscordSelectComponent("addDeregistrationForCurrontDayToDbDropdown", "Kind zum Abmelden auswählen", options);

            var message = new DiscordInteractionResponseBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.DarkBlue)
                .WithTitle("Welches Kind soll Abgemeldet werden"))
                .AddComponents(dropdown);

            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, message);
        }
    }
}
