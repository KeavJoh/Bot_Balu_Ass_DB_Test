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
            if(DateTime.Now.Hour == 13 && DateTime.Now.Minute == 50)
            {
                await MainMessageController.DeregistrationInformationDateTimeNowMainMessage();
                await MainMessageController.DeregistrationInformationFutureMainMessage();
                Console.WriteLine("Erledigt " + DateTime.Now);
            }
        }
    }
}
