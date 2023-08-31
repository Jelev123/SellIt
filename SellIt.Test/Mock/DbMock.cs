namespace SellIt.Test.Mock
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Infrastructure.Data;

    public class DbMock
    {
        public static ApplicationDbContext Instance 
        {
            get
            {
                var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ApplicationDbContext(dbContext);
            }
        }
    }
}
