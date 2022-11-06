using System.Collections.Generic;
using System.Linq;
using Athena.Core.Domain;
using Athena.Core.UseCases;
using Microsoft.EntityFrameworkCore;

namespace Athena.IO.DataAccess;

internal class EntityFrameworkRepository : IBacklogRepository
{
    private readonly BacklogContext myDbContext;

    public EntityFrameworkRepository()
    {
        myDbContext = new BacklogContext();
    }

    public IReadOnlyCollection<Improvement> GetBacklog()
    {
        return myDbContext.Improvements.ToList();
    }

    class BacklogContext : DbContext
    {
        public DbSet<Improvement> Improvements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: connect to database
        }
    }
}
