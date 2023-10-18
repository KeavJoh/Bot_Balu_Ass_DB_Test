using Bot_Balu_Ass_DB.Data.Database;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new ApplicationDbContext())
        {
            context.Database.EnsureCreated();
        }

    }
}
