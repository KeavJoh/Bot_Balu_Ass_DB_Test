using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.BotSettingsModels
{
    internal class ChannelSettings
    {
        public ulong ParentsCommandChannel {  get; set; }
        public ulong ParentsInformationChannel { get; set; }
        public ulong ExecutiveCommandChannel { get; set; }
        public ulong ExecutiveInformationChannel { get; set; }
        public ulong EmployeesCommandChannel { get; set; }
        public ulong EmployeesInformationChannel { get; set; }

    }
}
