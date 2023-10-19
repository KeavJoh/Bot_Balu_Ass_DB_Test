using Bot_Balu_Ass_DB.BotSettingsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bot_Balu_Ass_DB.InitialController
{
    internal class InitializeBotConfigController
    {
        public BotConfig InitializeBotConfig()
        {
            IConfiguration configuration = InitializeConfiguration();
            BotConfig botConfig = InitializebotConfig(configuration);

            return botConfig;
        }

        static IConfiguration InitializeConfiguration()
        {
            IConfiguration configuration = null;
            var builder = new ConfigurationBuilder();

            try
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("botsettings.json");

                configuration = builder.Build();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Die benötigte Datei ist nicht vorhanden! Programm wird beendet: " + ex.Message);
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Es ist ein Fehler beim Abrufen der benötigten Datei aufgetreten. Das Programm wird beendet: " + ex.Message);
                Environment.Exit(1);
            }

            return configuration;
        }

        static BotConfig InitializebotConfig(IConfiguration configuration)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var botConfig = configuration.Get<BotConfig>();
                if (botConfig == null)
                {
                    Console.WriteLine("BotConfig fehlerhaft. Programm wird beendet: ");
                    Environment.Exit(1);
                }
                services.AddSingleton(botConfig);
            })
            .Build();

            // Jetzt können Sie auf "botConfig" und "host" außerhalb der Funktion zugreifen
            var botConfig = host.Services.GetRequiredService<BotConfig>();

            return botConfig;
        }
    }
}
