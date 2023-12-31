﻿using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Controller;
using Bot_Balu_Ass_DB.Controller.ActionControllers;
using Bot_Balu_Ass_DB.Controller.EventControllers;
using Bot_Balu_Ass_DB.Controller.ModalControllers;
using Bot_Balu_Ass_DB.Data.Database;
using Bot_Balu_Ass_DB.InitialController;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    private static DiscordClient Client { get; set; }
    private static CommandsNextExtension Commands {  get; set; }
    private static Timer? Timer {  get; set; }
    static async Task Main(string[] args)
    {
        //init bot configuration
        InitializeBotConfigController botConfigController = new InitializeBotConfigController();
        BotConfig botConfig = botConfigController.InitializeBotConfig();
        ApplicationDbContext context = new ApplicationDbContext(botConfig);
        ClientReadyController clientReadyController = new ClientReadyController(botConfig, context);

        //init database
        await InitializeDatabaseController.InitializeDatabaseHandler(botConfig);

        //init bot
        InitializeBotFinalController botFinalController = new InitializeBotFinalController();
        var discordConfig = botFinalController.InitializeBotFinalHandler(botConfig);
        Client = new DiscordClient(discordConfig);
        await botFinalController.InitializeBotGlobalSettingsHandler(botConfig, context, Client);
        await GlobalDataStore.InitializeGlobalDataStore();

        Client.Ready += clientReadyController.ClientReadyHandler;
        Client.ComponentInteractionCreated += ButtonEventController.ButtonEventHandler;
        Client.ComponentInteractionCreated += SelectComponentEventController.SelectComponentEventHandler;
        Client.ModalSubmitted += ModalEventController.ModalEventHandler;

        var slashCommandsConfig = Client.UseSlashCommands();

        slashCommandsConfig.RegisterCommands<ExecutiveModalController>();

        Timer = new Timer(ShortTimer, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        await Client.ConnectAsync();
        await Task.Delay(-1);
    }

    private static async void ShortTimer(object state)
    {
        await TimerActionController.TimerActionHandler();
    }
}
