using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        BotConfig botConfig = new BotConfig();
        IConfiguration configuration = builder.Build();

        //init botsettings.json

        try
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("botsettings.json");

            configuration = builder.Build();
        } catch (FileNotFoundException ex)
        {
            Console.WriteLine("botsettings.json existiert nicht! Programm wird beendet");
            Environment.Exit(1);
        } catch (Exception ex)
        {
            Console.WriteLine("Es ist ein Fehler beim abrufen der botsettings.json aufgetreten. Das Programm wird beendet");
            Environment.Exit(1);
        }

        if (configuration != null)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var botConfig = configuration.Get<BotConfig>();
                if (botConfig == null)
                {
                    Console.WriteLine("BotConfig fehlerhaft. Programm wird beendet");
                    Environment.Exit(1);
                }
                services.AddSingleton(botConfig);
            })
            .Build();

            botConfig = host.Services.GetRequiredService<BotConfig>();
        }

        //init database
        using (var context = new ApplicationDbContext(botConfig))
        {
            context.Database.EnsureCreated();
        }
    }
}
