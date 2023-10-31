using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Database;
using Bot_Balu_Ass_DB.Data.Model;
using Bot_Balu_Ass_DB.InitialController;
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
    internal class ExecutiveActionController
    {
        public static async Task AddChildToDb(ModalSubmitEventArgs modal)
        {
            var context = GlobalSettings.Context;
            string childName = modal.Values["nameOfChild"];

            var newChild = new ChildModel
            {
                Name = childName,
            };

            await context.Children.AddAsync(newChild);
            await context.SaveChangesAsync();

            await modal.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Ich habe {childName} erfolgreich in die Datenbank eingeschrieben."));

            await GlobalDataStore.ReloadChildList();

            await Task.Delay(10000);
            await modal.Interaction.DeleteOriginalResponseAsync();

            return;
        }

        public static async Task DeleteChildFromDb(ComponentInteractionCreateEventArgs args)
        {
            var context = GlobalSettings.Context;
            var selectedChild = args.Values.FirstOrDefault();

            int.TryParse(selectedChild, out var childIdInDb);
            var childDb = context.Children.SingleOrDefault(x => x.Id == childIdInDb);
            var ChildInDbName = GlobalDataStore.ChildList.FirstOrDefault(x => x.Id == childIdInDb)?.Name;

            context.Children.Remove(childDb);
            await context.SaveChangesAsync();

            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent($"Ich habe {ChildInDbName} erfolgreich aus der Datenbank entfernt"));

            await GlobalDataStore.ReloadChildList();

            await Task.Delay(10000);
            await args.Interaction.DeleteOriginalResponseAsync();
        }

    }
}
