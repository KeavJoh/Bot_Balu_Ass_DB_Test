using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller.ActionControllers
{
    internal class TimerActionController
    {
        public static async Task TimerActionHandler()
        {
            if(DateTime.Now.Hour == 02 && DateTime.Now.Minute == 30)
            {
                await CheckOldDeregistrations();
                await GlobalDataStore.ReloadDeregistrationList();
                await LogController.LogTimerTaskResult(DateTime.Now + " Tägliche Prüfung und neu erstellen der Abmeldungen wurde durchgeführt");
            }
        }

        private static async Task CheckOldDeregistrations()
        {
            var context = GlobalSettings.Context;
            List<Deregistration> oldDeregistrations = context.Deregistrations.Where(x => x.DeregistrationDate.Date < DateTime.Now.Date).ToList();
            if(oldDeregistrations.Count > 0)
            {
                foreach(var deregistration in oldDeregistrations)
                {
                    CompleteDeregistration completeDeregistration = CompleteDeregistration.CreateCompleteDeregistration(deregistration);
                    await context.completeDeregistrations.AddAsync(completeDeregistration);
                    context.Deregistrations.Remove(deregistration);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
