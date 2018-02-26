namespace HitmanService.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
