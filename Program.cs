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
        InitializeBotConfigController botConfigController = new InitializeBotConfigController();
        BotConfig botConfig = botConfigController.InitializeBotConfig();

        //init database
        await InitializeDatabaseController.InitializeDatabaseHandler(botConfig);

        //init bot

        var discordConfig = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = botConfig.botSettings.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
        };

        Client = new DiscordClient(discordConfig);

        Client.Ready += ClientReadyController.ClientReadyHandler;
        Client.ComponentInteractionCreated += ButtonEventController.ButtonEventHandler;
        Client.ModalSubmitted += ModalEventController.ModalEventHandler;

        await Client.ConnectAsync();
        await Task.Delay(-1);
    }
}
