namespace Core.DataAccess.Tests
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public interface IDbContextTestImplementation : IDbContext<ITestDbToken>
    {
        DbSet<TestDatabaseEntity> TestDatabaseEntity { get; set; }
    }
}
