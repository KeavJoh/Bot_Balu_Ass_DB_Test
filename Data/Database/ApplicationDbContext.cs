using Bot_Balu_Ass_DB.BotSettingsModels;
using Microsoft.EntityFrameworkCore;

namespace Bot_Balu_Ass_DB.Data.Database
{
    internal class ApplicationDbContext : DbContext
    {
        private readonly BotConfig _botConfig;

        public ApplicationDbContext (BotConfig botConfig)
        {
            _botConfig = botConfig;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_botConfig.DatabaseConnectionString.DefaultDatabaseConnection);
        }
    }
}
