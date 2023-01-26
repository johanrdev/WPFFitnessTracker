using System;

namespace FitnessTracker.Application.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IReportRepository Reports { get; }
        int Complete();
    }
}
