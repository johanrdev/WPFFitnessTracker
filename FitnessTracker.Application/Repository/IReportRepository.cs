using FitnessTracker.Domain.Models;
using System;
using System.Collections.Generic;

namespace FitnessTracker.Application.Repository
{
    public interface IReportRepository : IRepository<Report>
    {
        IEnumerable<Report> GetReportsBetweenDates(DateTime minDate, DateTime maxDate);
    }
}
