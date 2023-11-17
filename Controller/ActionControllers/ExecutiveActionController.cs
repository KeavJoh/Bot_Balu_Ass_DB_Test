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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller.ActionControllers
{
    internal class ExecutiveActionController
    {
        private static readonly ApplicationDbContext context = GlobalSettings.Context;

        public static async Task AddChildToDbAction(ModalSubmitEventArgs modal)
        {
            string childName = modal.Values["nameOfChild"];
            string childMother = modal.Values["nameOfMother"];
            string childFather = modal.Values["nameOfFather"];

            var newChild = new ChildModel
            {
                Name = childName,
                Mother = childMother,
                Father = childFather,
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

        public static async Task DeleteChildFromDbAction(ComponentInteractionCreateEventArgs args)
        {
            var selectedChild = args.Values.FirstOrDefault();

            int.TryParse(selectedChild, out var childIdInDb);
            var childDb = context.Children.SingleOrDefault(x => x.Id == childIdInDb);
            var ChildInDbName = GlobalDataStore.ChildList.FirstOrDefault(x => x.Id == childIdInDb)?.Name;
            var activeDeregistrations = GlobalDataStore.DeregistrationList.Where(x => x.ChildId == childIdInDb).ToList();

            foreach (var deregistration in activeDeregistrations)
            {
                var newWithdrawDeregistration = new WithdrawnDeregistration
                {
                    ChildName = deregistration.ChildName,
                    ChildId = deregistration.ChildId,
                    DeregistrationDate = deregistration.DeregistrationDate,
                    DeregistrationReason = deregistration.Reason,
                    DateOfDeregistrationAction = deregistration.DateOfAction,
                    DateOfWithdrawn = DateTime.Now.Date,
                    DeregistrationPerformedFromParents = deregistration.DeregistrationPerformedFromParents,
                    WithdrawnPerformedFromParents = false
                };

                await context.WithdrawnDeregistrations.AddAsync(newWithdrawDeregistration);
                context.Deregistrations.Remove(deregistration);
            }

            context.Children.Remove(childDb);
            await context.SaveChangesAsync();

            if (activeDeregistrations.Count != 0)
            {
                await GlobalDataStore.ReloadDeregistrationList();
            }

            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent($"Ich habe {ChildInDbName} erfolgreich aus der Datenbank entfernt"));

            await GlobalDataStore.ReloadChildList();

            await Task.Delay(10000);
            await args.Interaction.DeleteOriginalResponseAsync();
        }

        public static async Task AddImportandDateToDbAction(ModalSubmitEventArgs args)
        {
            string importandDateEventName = args.Values["eventName"];
            string? importandDateComment = null;
            DateTime importandDateDateFrom = HelpFunctionController.ParseStringToDateTimeHelper(args.Values["dateFrom"]);
            DateTime? importdandDateDateTo = null;

            if (args.Values["comment"] != "")
            {
                importandDateComment = args.Values["comment"];
            }

            if (args.Values["dateTo"] != "")
            {
                importdandDateDateTo = HelpFunctionController.ParseStringToDateTimeHelper(args.Values["dateTo"]);
            }

            var newImportandDate = new ImportandDate
            {
                EventName = importandDateEventName,
                EventComment = importandDateComment,
                Date = importandDateDateFrom,
                DateTo = importdandDateDateTo
            };

            await context.ImportandDates.AddAsync(newImportandDate);
            await context.SaveChangesAsync();
            await GlobalDataStore.ReloadImportandDateList();

            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent($"Ich habe den Termin {importandDateEventName} erstellt"));

            await Task.Delay(10000);
            await args.Interaction.DeleteOriginalResponseAsync();
            
        }

    }
}
