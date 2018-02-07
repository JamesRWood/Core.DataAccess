namespace Hades.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    public abstract class DbContextBase : DbContext
    {
        private readonly string _connectionString;

        protected DbContextBase(
            string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
