using FitnessTracker.Application.Repository;

namespace FitnessTracker.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitnessTrackerDbContext _context;

        public IReportRepository Reports { get; private set; }

        public UnitOfWork(FitnessTrackerDbContext context)
        {
            _context = context;

            Reports = new ReportRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
