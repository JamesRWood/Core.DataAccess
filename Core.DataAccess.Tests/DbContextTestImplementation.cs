namespace Core.DataAccess.Tests
{
    using Contracts;
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
