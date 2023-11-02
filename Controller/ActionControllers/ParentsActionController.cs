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
using System.Reflection.Metadata.Ecma335;

namespace Bot_Balu_Ass_DB.Controller.ActionControllers
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

            if (dateTo == DateTime.MinValue || dateTo == dateFrom)
            {
                var existingDeregistration = GlobalDataStore.DeregistrationList.FirstOrDefault(d => d.ChildName == childName && d.DeregistrationDate == dateFrom);

                if (existingDeregistration == null && (dateFrom.DayOfWeek != DayOfWeek.Saturday || dateFrom.DayOfWeek != DayOfWeek.Sunday || dateFrom != DateTime.Now))
                {
                    var newDeregistration = new Deregistration
                    {
                        ChildName = childName,
                        ChildId = GlobalDataStore.ChildList.FirstOrDefault(x => x.Name == childName).Id,
                        Reason = reason,
                        DeregistrationDate = dateFrom,
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
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                         .WithContent($"Für den angegebenen Zeitraum existiert für {childName} bereits eine Abmeldung oder es handelt sich um einen Samstag/Sonntag"));

                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                }
            }
            else
            {
                DateTime dateToCheck = dateFrom;
                var dates = new List<DateTime>();

                while(dateToCheck <= dateTo)
                {
                    if ((dateToCheck.DayOfWeek != DayOfWeek.Sunday && dateToCheck.DayOfWeek != DayOfWeek.Saturday) || dateToCheck < DateTime.Now)
                    {
                        var existingDeregistration = GlobalDataStore.DeregistrationList.FirstOrDefault(d => d.ChildName == childName && d.DeregistrationDate == dateToCheck);
                        if(existingDeregistration == null)
                        {
                            dates.Add(dateToCheck);
                            dateToCheck = dateToCheck.AddDays(1);
                        }
                        else
                        {
                            dateToCheck = dateToCheck.AddDays(1);
                            continue;
                        }
                    }
                    else
                    {
                        dateToCheck = dateToCheck.AddDays(1);
                    }
                }

                if(dates.Count != 0)
                {
                    foreach (var date in dates)
                    {
                        var newDeregistration = new Deregistration
                        {
                            ChildName = childName,
                            ChildId = GlobalDataStore.ChildList.FirstOrDefault(x => x.Name == childName).Id,
                            Reason = reason,
                            DeregistrationDate = date,
                            DeregistrationPerformedFromParents = true,
                            DateOfAction = DateTime.Now,
                        };

                        await context.Deregistrations.AddAsync(newDeregistration);
                        await context.SaveChangesAsync();
                    }

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Ich habe {childName} erfolgreich Abgemeldet"));
                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                }
                else
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Für den angegebenen Zeitraum existiert bereits eine Abmeldung für {childName}"));
                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                }
            }
        }

        public static async Task CompleteRegistrationToDbAction(ModalSubmitEventArgs args)
        {
            var context = GlobalSettings.Context;
            var values = args.Values;


        }
    }
}
