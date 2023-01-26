using FitnessTracker.Domain.Models;
using System.Data.Entity;

namespace FitnessTracker.Infrastructure.Repository
{
    public class FitnessTrackerDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }

        public FitnessTrackerDbContext() : base("FitnessTracker_DB") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
