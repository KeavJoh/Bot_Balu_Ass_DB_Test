using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Controller;
using Bot_Balu_Ass_DB.Data.Database;
using Bot_Balu_Ass_DB.InitialController;
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
        //init bot configuration
        InitializeBotConfigController botConfigController = new InitializeBotConfigController();
        BotConfig botConfig = botConfigController.InitializeBotConfig();
        ApplicationDbContext context = new ApplicationDbContext(botConfig);
        ClientReadyController clientReadyController = new ClientReadyController(context, botConfig);

        //init database
        await InitializeDatabaseController.InitializeDatabaseHandler(botConfig);

        //init bot
        InitializeBotFinalController botFinalController = new InitializeBotFinalController();
        var discordConfig = botFinalController.InitializeBotFinalHandler(botConfig);

        Client = new DiscordClient(discordConfig);

        Client.Ready += clientReadyController.ClientReadyHandler;
        Client.ComponentInteractionCreated += ButtonEventController.ButtonEventHandler;
        Client.ModalSubmitted += ModalEventController.ModalEventHandler;

        await Client.ConnectAsync();
        await Task.Delay(-1);
    }
}
