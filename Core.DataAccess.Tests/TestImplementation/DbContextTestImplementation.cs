namespace Core.DataAccess.Tests.TestImplementation
{
    using Microsoft.EntityFrameworkCore;

    public class DbContextTestImplementation : DbContextBase, IDbContextTestImplementation
    {
        public DbContextTestImplementation(
            string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<TestDatabaseEntity> TestDatabaseEntity { get; set; }
    }
}
