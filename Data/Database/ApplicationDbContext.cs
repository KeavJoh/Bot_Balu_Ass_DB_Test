using Microsoft.EntityFrameworkCore;

namespace Bot_Balu_Ass_DB.Data.Database
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BotBaluAssDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
