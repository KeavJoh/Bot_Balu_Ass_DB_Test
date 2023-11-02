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
using Microsoft.EntityFrameworkCore;
using Bot_Balu_Ass_DB.Controller.EventControllers;

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

            if (dateFrom < DateTime.Now)
            {
                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                    .WithContent($"Das angegebene Datum liegt in der Vergangenheit. Eine Abmeldung für die Vergangenheit ist nicht möglich!"));

                await Task.Delay(10000);
                await args.Interaction.DeleteOriginalResponseAsync();
                return;
            }

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

                    await GlobalDataStore.ReloadDeregistrationList();

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

                    await GlobalDataStore.ReloadDeregistrationList();

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

            var childName = values["nameOfChild"];
            DateTime dateFrom = HelpFunctionController.ParseStringToDateTime(values["dateFrom"]);
            DateTime dateTo = HelpFunctionController.ParseStringToDateTime(values["dateTo"]);

            if(dateTo == DateTime.MinValue || dateTo == dateFrom)
            {
                var selectedDeregistration = GlobalDataStore.DeregistrationList.FirstOrDefault(d => d.ChildName == childName && d.DeregistrationDate == dateFrom);
                if (selectedDeregistration != null)
                {
                    var selectedDeregistrationInDb = context.Deregistrations.FirstOrDefault(d => d.Id == selectedDeregistration.Id);
                    var newWithdrawDeregistration = new WithdrawnDeregistration()
                    {
                        ChildName = selectedDeregistration.ChildName,
                        ChildId = selectedDeregistration.ChildId,
                        DeregistrationDate = selectedDeregistration.DeregistrationDate,
                        DeregistrationReason = selectedDeregistration.Reason,
                        DateOfDeregistrationAction = selectedDeregistration.DateOfAction,
                        DateOfWithdrawn = DateTime.Now,
                        DeregistrationPerformedFromParents = selectedDeregistration.DeregistrationPerformedFromParents,
                        WithdrawnPerformedFromParents = true
                    };

                    await context.WithdrawnDeregistrations.AddAsync(newWithdrawDeregistration);
                    context.Deregistrations.Remove(selectedDeregistration);
                    await context.SaveChangesAsync();

                    await GlobalDataStore.ReloadDeregistrationList();

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Ich habe {childName} für den angegebenen Zeitraum wieder Angemeldet"));
                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                }
                else
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Es gibt für den Zeitraum keine Abmeldung für {childName}! Eine Anmeldung ist somit nicht nötig"));
                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                }
            }
            else
            {
                DateTime dateToCheck = dateFrom;
                var deregistrationForWithdrawn = new List<Deregistration>();

                while (dateToCheck <= dateTo)
                {
                        var existingDeregistration = GlobalDataStore.DeregistrationList.FirstOrDefault(d => d.ChildName == childName && d.DeregistrationDate == dateToCheck);
                        if (existingDeregistration == null)
                        {
                            dateToCheck = dateToCheck.AddDays(1);
                            continue;
                        }
                        else
                        {
                            deregistrationForWithdrawn.Add(existingDeregistration);
                            dateToCheck = dateToCheck.AddDays(1);
                    }
                }

                if(deregistrationForWithdrawn.Count != 0)
                {
                    foreach (var data in deregistrationForWithdrawn)
                    {
                        var selectedDeregistrationInDb = context.Deregistrations.FirstOrDefault(d => d.Id == data.Id);
                        var newWithdrawDeregistration = new WithdrawnDeregistration()
                        {
                            ChildName = data.ChildName,
                            ChildId = data.ChildId,
                            DeregistrationDate = data.DeregistrationDate,
                            DeregistrationReason = data.Reason,
                            DateOfDeregistrationAction = data.DateOfAction,
                            DateOfWithdrawn = DateTime.Now,
                            DeregistrationPerformedFromParents = data.DeregistrationPerformedFromParents,
                            WithdrawnPerformedFromParents = true
                        };

                        await context.WithdrawnDeregistrations.AddAsync(newWithdrawDeregistration);
                        context.Deregistrations.Remove(selectedDeregistrationInDb);
                    }

                    await context.SaveChangesAsync();

                    await GlobalDataStore.ReloadDeregistrationList();

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Ich habe {childName} für den angegebenen Zeitraum wieder Angemeldet"));
                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                }
                else
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Es gibt für den Zeitraum keine Abmeldung für {childName}! Eine Anmeldung ist somit nicht nötig"));
                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                }
            }
        }
    }
}
