using FitnessTracker.Application.Repository;
using FitnessTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTracker.Infrastructure.Repository
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public FitnessTrackerDbContext FitnessTrackerDbContext
        {
            get { return Context as FitnessTrackerDbContext; }
        }

        public ReportRepository(FitnessTrackerDbContext context) : base(context) { }

        public IEnumerable<Report> GetReportsBetweenDates(DateTime minDate, DateTime maxDate)
        {
            return FitnessTrackerDbContext.Reports
                .Where(r => r.Date >= minDate && r.Date <= maxDate)
                .OrderBy(r => r.Date)
                .ToList();
        }
    }
}
