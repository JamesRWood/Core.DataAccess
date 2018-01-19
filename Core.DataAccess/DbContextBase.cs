namespace Core.DataAccess
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public abstract class DbContextBase<TContext> : DbContext where TContext : DbContextBase<TContext>
    {
        private readonly string _connectionString;

        protected DbContextBase(
            IEnumerable<DbSet<IDbSet>> databaseEntities,
            string connectionString)
        {
            DatabaseEntities = databaseEntities;
            _connectionString = connectionString;
        }

        public IEnumerable<DbSet<IDbSet>> DatabaseEntities { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var databaseEntity in DatabaseEntities)
            {
                var entitytype = databaseEntity.GetType();
                modelBuilder.Entity(entitytype).ToTable(entitytype.Name);
            }
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
