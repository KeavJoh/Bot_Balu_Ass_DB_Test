using Bot_Balu_Ass_DB.Controller;
using Bot_Balu_Ass_DB.Data.Database;
using Bot_Balu_Ass_DB.Data.Model;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.BotSettingsModels
{
    internal static class GlobalDataStore
    {
        private static ApplicationDbContext context = GlobalSettings.Context;

        public static List<ChildModel> ChildList { get; set; }
        public static List<Deregistration> DeregistrationList { get; set; }

        public static async Task InitializeGlobalDataStore()
        {
            ChildList = await context.Children.OrderBy(x => x.Name).ToListAsync();
            DeregistrationList = await context.Deregistrations.OrderBy(x => x.DeregistrationFrom).ToListAsync();
        }

        public static async Task ReloadChildList()
        {
            ChildList = await context.Children.OrderBy(x => x.Name).ToListAsync();

            await MainMessageController.ParentsMainMessage();
        }

        public static async Task ReloadDeregistrationList()
        {
            DeregistrationList = await context.Deregistrations.OrderBy(x => x.DeregistrationFrom).ToListAsync();
        }

        public static async Task<List<DiscordSelectComponentOption>> GetChildList()
        {
            List<ChildModel> childsFromDb = ChildList;

            var options = new List<DiscordSelectComponentOption>();

            foreach (var child in childsFromDb)
            {
                options.Add(new DiscordSelectComponentOption(child.Name, child.Id.ToString()));
            }

            return options;
        }

    }
}
