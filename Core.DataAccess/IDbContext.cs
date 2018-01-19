namespace Core.DataAccess
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public interface IDbContext
    {
        IEnumerable<DbSet<IDbSet>> DatabaseEntities { get; }
    }
}
