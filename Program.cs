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
        //init botsettings.json

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("botsettings.json");

        var configuration = builder.Build();

        var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            services.AddSingleton(configuration.Get<BotConfig>());
        })
        .Build();

        var botConfig = host.Services.GetRequiredService<BotConfig>();

        //init database
        using (var context = new ApplicationDbContext(botConfig))
        {
            context.Database.EnsureCreated();
        }



    }
}
