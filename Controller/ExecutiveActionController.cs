using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Database;
using Bot_Balu_Ass_DB.Data.Model;
using Bot_Balu_Ass_DB.InitialController;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
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
                        .WithContent($"Leider ist ein Fehler aufgetreten. Das Datum test ist kein gültiges Datum."));

            return;
        }

    }
}
