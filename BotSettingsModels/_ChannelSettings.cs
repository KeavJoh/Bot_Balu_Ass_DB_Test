using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.BotSettingsModels
{
    internal class _ChannelSettings
    {
        public long ParentsCommandChannel {  get; set; }
        public long ParentsInformationChannel { get; set; }
        public long ExecutiveCommandChannel { get; set; }
        public long ExecutiveInformationChannel { get; set; }
        public long EmployeesCommandChannel { get; set; }
        public long EmployeesInformationChannel { get; set; }

    }
}
