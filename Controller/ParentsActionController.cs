using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Model;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class ParentsActionController
    {
        public static async Task AddDeregistrationChildToDbAction(ModalSubmitEventArgs args)
        {
            var context = GlobalSettings.Context;
            var values = args.Values;

            var childName = values["nameOfChild"];
            DateTime dateFrom = HelpFunctionController.ParseStringToDateTime(values["dateFrom"]);
            DateTime dateTo = HelpFunctionController.ParseStringToDateTime(values["dateTo"]);
            var reason = values["reason"];
            var existingDeregistration = GlobalDataStore.DeregistrationList.FirstOrDefault(d => d.DeregistrationForOneDay == true && d.DeregistrationFrom == dateFrom || d.DeregistrationForOneDay == false && d.DeregistrationFrom <= dateFrom && d.DeregistrationTo >= dateFrom);

            if (existingDeregistration != null)
            {
                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                   .WithContent($"{childName} ist für den Zeitraum bereits abgemeldet. Eine weitere Abmeldung ist nicht nötig!"));

                await Task.Delay(10000);
                await args.Interaction.DeleteOriginalResponseAsync();
            }
            else if (dateTo == DateTime.MinValue || dateTo == dateFrom)
            {
                var newDeregistration = new Deregistration
                {
                    ChildName = childName,
                    ChildId = GlobalDataStore.ChildList.FirstOrDefault(x => x.Name == childName).Id,
                    Reason = reason,
                    DeregistrationFrom = dateFrom,
                    DeregistrationForOneDay = true,
                    DeregistrationPerformedFromParents = true,
                    DateOfAction = DateTime.Now,
                };

                await context.Deregistrations.AddAsync(newDeregistration);
                await context.SaveChangesAsync();

                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                    .WithContent($"Ich habe {childName} erfolgreich Abgemeldet"));

                await Task.Delay(10000);
                await args.Interaction.DeleteOriginalResponseAsync();
            } 
            else
            {
                var newDeregistration = new Deregistration
                {
                    ChildName = childName,
                    ChildId = GlobalDataStore.ChildList.FirstOrDefault(x => x.Name == childName).Id,
                    Reason = reason,
                    DeregistrationFrom = dateFrom,
                    DeregistrationTo = dateTo,
                    DeregistrationForOneDay = false,
                    DeregistrationPerformedFromParents = true,
                    DateOfAction = DateTime.Now,
                };

                await context.Deregistrations.AddAsync (newDeregistration);
                await context.SaveChangesAsync();

                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                    .WithContent($"Ich habe {childName} erfolgreich Abgemeldet"));

                await Task.Delay(10000);
                await args.Interaction.DeleteOriginalResponseAsync();
            }
        }
    }
}
