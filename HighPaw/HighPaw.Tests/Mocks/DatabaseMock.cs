namespace HighPaw.Tests.Mocks
{
    using HighPaw.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class DatabaseMock
    {
        public static HighPawDbContext Instance
        {
            get
            {
                var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
                var dbContext = new HighPawDbContext(options);

                return dbContext;
            }
        }
    }
}
