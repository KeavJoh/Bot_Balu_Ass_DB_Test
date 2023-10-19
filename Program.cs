using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Database;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    private static DiscordClient Client { get; set; }
    private static CommandsNextExtension Commands {  get; set; }
    static async Task Main(string[] args)
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
            Console.WriteLine("Die benötigte Datei ist nicht vorhanden! Programm wird beendet");
            Environment.Exit(1);
        } catch (Exception ex)
        {
            Console.WriteLine("Es ist ein Fehler beim abrufen der benötigten Datei aufgetreten. Das Programm wird beendet");
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

        //init bot

        var discordConfig = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = botConfig.botSettings.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
        };

        Client = new DiscordClient(discordConfig);

        Client.Ready += Client_Ready;

        await Client.ConnectAsync();
        await Task.Delay(-1);
    }

    private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
    {
        return Task.CompletedTask;
    }
}
