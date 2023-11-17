using Bot_Balu_Ass_DB.BotSettingsModels;
using Bot_Balu_Ass_DB.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Bot_Balu_Ass_DB.Data.Database
{
    internal class ApplicationDbContext : DbContext
    {
        private readonly BotConfig _botConfig;

        public DbSet<ChildModel> Children { get; set; }
        public DbSet<Deregistration> Deregistrations { get; set; }
        public DbSet<WithdrawnDeregistration> WithdrawnDeregistrations { get; set; }
        public DbSet<CompleteDeregistration> CompleteDeregistrations { get; set; }
        public DbSet<ImportandDate> ImportandDates { get; set; }

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
