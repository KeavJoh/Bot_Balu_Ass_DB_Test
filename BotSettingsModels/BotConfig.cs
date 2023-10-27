using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.BotSettingsModels
{
    internal class BotConfig
    {
        public DatabaseConnectionString DatabaseConnectionString { get; set; }
        public BotSettings BotSettings { get; set; }
        public ChannelSettings ChannelSettings { get; set; }
    }
}
