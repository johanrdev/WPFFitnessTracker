using FitnessTracker.Domain.Models;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace FitnessTracker.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FitnessTracker.Infrastructure.Repository.FitnessTrackerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FitnessTracker.Infrastructure.Repository.FitnessTrackerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var reports = new Collection<Report>
            {
                new Report { Id = 1, Date = DateTime.Today.AddDays(-8), Weight = 78.8 },
                new Report { Id = 2, Date = DateTime.Today.AddDays(-7), Weight = 79.1 },
                new Report { Id = 3, Date = DateTime.Today.AddDays(-6), Weight = 79.5 },
                new Report { Id = 4, Date = DateTime.Today.AddDays(-5), Weight = 80.2 },
                new Report { Id = 5, Date = DateTime.Today.AddDays(-4), Weight = 80.0 },
                new Report { Id = 6, Date = DateTime.Today.AddDays(-3), Weight = 79.6 },
                new Report { Id = 7, Date = DateTime.Today.AddDays(-2), Weight = 79.9 },
                new Report { Id = 8, Date = DateTime.Today.AddDays(-1), Weight = 79.2 },
                new Report { Id = 9, Date = DateTime.Today, Weight = 79.5 },
            };

            foreach (Report report in reports)
                context.Reports.AddOrUpdate(r => r.Id, report);

            context.SaveChanges();
        }
    }
}
