using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.BotSettingsModels
{
    internal class BotConfig
    {
        public _DatabaseConnectionString databaseConnectionString { get; set; }
        public _BotSettings botSettings { get; set; }
        public _ChannelSettings channelSettings { get; set; }
    }
}
