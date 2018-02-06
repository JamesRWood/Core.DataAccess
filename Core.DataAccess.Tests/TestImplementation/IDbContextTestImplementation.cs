namespace Core.DataAccess.Tests.TestImplementation
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public interface IDbContextTestImplementation : IDbContext<ITestDbToken>
    {
        DbSet<TestDatabaseEntity> TestDatabaseEntity { get; set; }
    }
}
