using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Controller
{
    internal class LogController
    {
        public static async Task LogTimerTaskResult(string message)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logFile = Path.Combine(currentDirectory, "log.txt");

            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                await writer.WriteLineAsync(message);
            }
        }
    }
}
