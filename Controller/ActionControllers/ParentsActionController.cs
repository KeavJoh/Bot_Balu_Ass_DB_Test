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
using Bot_Balu_Ass_DB.ValidationController;

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

            if (DeregistrationRegistrationValidation.DateIsInThePast(dateFrom))
            {
                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                    .WithContent($"Das angegebene Datum liegt in der Vergangenheit. Eine Abmeldung für die Vergangenheit ist nicht möglich!"));

                await Task.Delay(10000);
                await args.Interaction.DeleteOriginalResponseAsync();
                return;
            }

            if (DeregistrationRegistrationValidation.DeregistrationRegistrationForOneDay(dateFrom, dateTo))
            {
                if (dateFrom.DayOfWeek == DayOfWeek.Sunday || dateFrom.DayOfWeek == DayOfWeek.Saturday)
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Das angegebene Datum ist ein Samstag oder Sonntag! Für diese Tage wird keine Abmeldung benötigt"));

                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                    return;
                }

                var existingDeregistration = GlobalDataStore.DeregistrationList.FirstOrDefault(d => d.ChildName == childName && d.DeregistrationDate == dateFrom);

                if (existingDeregistration == null && (dateFrom.DayOfWeek != DayOfWeek.Saturday || dateFrom.DayOfWeek != DayOfWeek.Sunday || dateFrom != DateTime.Now.Date))
                {
                    var newDeregistration = Deregistration.CreateDeregistration(childName, GlobalDataStore.ChildList.FirstOrDefault(x => x.Name == childName).Id, reason, dateFrom, DateTime.Now, true);

                    await context.Deregistrations.AddAsync(newDeregistration);
                    await context.SaveChangesAsync();

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Ich habe {childName} erfolgreich Abgemeldet"));
                }
                else
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                         .WithContent($"Für den angegebenen Zeitraum existiert für {childName} bereits eine Abmeldung oder es handelt sich um einen Samstag/Sonntag"));

                }
            }
            else
            {
                DateTime dateToCheck = dateFrom;
                var dates = new List<DateTime>();

                if (DeregistrationRegistrationValidation.DateToIsLowerDateFrom(dateTo, dateFrom))
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Der angegebene Zeitraum passt nicht zusammen. Bitte überprüfe den angegebenen Zeitraum auf beispielsweise Fehler in den Jahreszahlen ({dateFrom.ToString("dd.MM.yyyy")} - {dateTo.ToString("dd.MM.yyyy")})"));

                    await Task.Delay(10000);
                    await args.Interaction.DeleteOriginalResponseAsync();
                    return;
                }

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
                        var newDeregistration = Deregistration.CreateDeregistration(childName, GlobalDataStore.ChildList.FirstOrDefault(x => x.Name == childName).Id, reason, date, DateTime.Now, true);

                        await context.Deregistrations.AddAsync(newDeregistration);
                        await context.SaveChangesAsync();
                    }

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Ich habe {childName} erfolgreich Abgemeldet"));
                }
                else
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Für den angegebenen Zeitraum existiert bereits eine Abmeldung für {childName}"));
                }
            }

            await GlobalDataStore.ReloadDeregistrationList();
            await Task.Delay(10000);
            await args.Interaction.DeleteOriginalResponseAsync();
        }

        public static async Task CompleteRegistrationToDbAction(ModalSubmitEventArgs args)
        {
            var context = GlobalSettings.Context;
            var values = args.Values;

            var childName = values["nameOfChild"];
            DateTime dateFrom = HelpFunctionController.ParseStringToDateTime(values["dateFrom"]);
            DateTime dateTo = HelpFunctionController.ParseStringToDateTime(values["dateTo"]);

            if(DeregistrationRegistrationValidation.DeregistrationRegistrationForOneDay(dateFrom, dateTo))
            {
                var selectedDeregistration = GlobalDataStore.DeregistrationList.FirstOrDefault(d => d.ChildName == childName && d.DeregistrationDate == dateFrom);
                if (selectedDeregistration != null)
                {
                    var selectedDeregistrationInDb = context.Deregistrations.FirstOrDefault(d => d.Id == selectedDeregistration.Id);
                    var newWithdrawDeregistration = WithdrawnDeregistration.CreateWithdrawnDeregistration(selectedDeregistrationInDb.ChildName, selectedDeregistrationInDb.ChildId, selectedDeregistrationInDb.Reason, selectedDeregistrationInDb.DeregistrationDate, selectedDeregistrationInDb.DateOfAction, selectedDeregistrationInDb.DeregistrationPerformedFromParents, true);

                    await context.WithdrawnDeregistrations.AddAsync(newWithdrawDeregistration);
                    context.Deregistrations.Remove(selectedDeregistration);
                    await context.SaveChangesAsync();

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Ich habe {childName} für den angegebenen Zeitraum wieder Angemeldet"));
                }
                else
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Es gibt für den Zeitraum keine Abmeldung für {childName}! Eine Anmeldung ist somit nicht nötig"));
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
                        var newWithdrawDeregistration = WithdrawnDeregistration.CreateWithdrawnDeregistration(selectedDeregistrationInDb.ChildName, selectedDeregistrationInDb.ChildId, selectedDeregistrationInDb.Reason, selectedDeregistrationInDb.DeregistrationDate, selectedDeregistrationInDb.DateOfAction, selectedDeregistrationInDb.DeregistrationPerformedFromParents, true);

                        await context.WithdrawnDeregistrations.AddAsync(newWithdrawDeregistration);
                        context.Deregistrations.Remove(selectedDeregistrationInDb);
                    }

                    await context.SaveChangesAsync();

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Ich habe {childName} für den angegebenen Zeitraum wieder Angemeldet"));
                }
                else
                {
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"Es gibt für den Zeitraum keine Abmeldung für {childName}! Eine Anmeldung ist somit nicht nötig"));
                }
            }

            await GlobalDataStore.ReloadDeregistrationList();
            await Task.Delay(10000);
            await args.Interaction.DeleteOriginalResponseAsync();
        }

        public static async Task AddDeregistrationChildForCurrentDayToDbAction(ComponentInteractionCreateEventArgs args)
        {
            var context = GlobalSettings.Context;
            var selectedChild = args.Values.FirstOrDefault();
            int.TryParse(selectedChild, out var childIdInDb);
            var childDb = context.Children.SingleOrDefault(x => x.Id == childIdInDb);
            var ChildInDbName = GlobalDataStore.ChildList.FirstOrDefault(x => x.Id == childIdInDb)?.Name;
            var existingDeregistration = GlobalDataStore.DeregistrationList.FirstOrDefault(d => d.ChildName == ChildInDbName && d.DeregistrationDate == DateTime.Now.Date);

            if (existingDeregistration != null)
            {
                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                     .WithContent($"Für den angegebenen Zeitraum existiert für {ChildInDbName} bereits eine Abmeldung oder es handelt sich um einen Samstag/Sonntag"));

                await Task.Delay(10000);
                await args.Interaction.DeleteOriginalResponseAsync();
                return;
            }
            else
            {
                var newDeregistration = Deregistration.CreateDeregistration(ChildInDbName, childIdInDb, "Schnellabmeldung", DateTime.Now.Date, DateTime.Now, true);

                await context.Deregistrations.AddAsync(newDeregistration);
                await context.SaveChangesAsync();

                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                     .WithContent($"Ich habe {ChildInDbName} für den angegebenen Zeitraum abgemeldet"));
            }

            await GlobalDataStore.ReloadDeregistrationList();
            await Task.Delay(10000);
            await args.Interaction.DeleteOriginalResponseAsync();
        }
    }
}
