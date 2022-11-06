using System.Linq;
using Athena.Adapters.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Athena.IO.DataAccess;

internal class EntityFrameworkRepository : IDatabase
{
    private readonly BacklogContext myDbContext;

    public EntityFrameworkRepository()
    {
        myDbContext = new BacklogContext();
    }

    public IQueryable<ImprovementDTO> GetBacklog() =>
        myDbContext.Improvements;

    class BacklogContext : DbContext
    {
        public DbSet<ImprovementDTO> Improvements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: connect to database
        }
    }
}
