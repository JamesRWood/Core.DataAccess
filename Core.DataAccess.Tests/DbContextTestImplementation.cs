namespace Core.DataAccess.Tests
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class DbContextTestImplementation : DbContextBase<DbContextTestImplementation>, ITestDbContext
    {
        public DbContextTestImplementation(
            IEnumerable<DbSet<IDbSet>> databaseEntities, 
            string connectionString)
            : base(databaseEntities, connectionString)
        {
        }
    }
}
