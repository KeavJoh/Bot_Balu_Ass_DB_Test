using Bot_Balu_Ass_DB.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.BotSettingsModels
{
    internal static class GlobalSettings
    {
        public static ApplicationDbContext Context { get; set; }
        public static BotConfig BotConfig { get; set; }

    }
}
