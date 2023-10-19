using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.InitialController
{
    internal class InitializeDatabaseController
    {
        public static Task InitializeDatabaseHandler(BotConfig botConfig)
        {
            try
            {
                using var context = new ApplicationDbContext(botConfig);
                context.Database.EnsureCreated();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine("Es ist ein Problem mit der Verbindung zur Datenbank aufgetreten: " + ex.Message);
                Environment.Exit(1);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine("Es ist ein Fehler in der Datenbankkontextkonfiguration aufgetrten: " + ex.Message);
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Es ist ein fehler beim erstellen der Datenbank aufgetreten: " + ex.Message);
                Environment.Exit(1);
            }

            return Task.CompletedTask;
        }
    }
}
