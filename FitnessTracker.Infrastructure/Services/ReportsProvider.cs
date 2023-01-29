using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Models;
using FitnessTracker.Infrastructure.Repository;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FitnessTracker.Infrastructure.Services
{
    public class ReportsProvider : BindableBase, IDataProvider<Report>
    {
        private bool _isLoading = false;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ObservableCollection<Report> Data { get; }

        public ReportsProvider()
        {
            Data = new ObservableCollection<Report>();
        }

        public IEnumerable<Report> LoadData()
        {
            var unitOfWork = new UnitOfWork(new FitnessTrackerDbContext());

            return unitOfWork.Reports.GetAll().OrderBy(r => r.Date);
        }

        public void SetData(IEnumerable<Report> data)
        {
            Data.Clear();

            foreach (var item in data) Data.Add(item);
        }

        public int Add(Report item)
        {
            var unitOfWork = new UnitOfWork(new FitnessTrackerDbContext());

            unitOfWork.Reports.Add(item);

            return unitOfWork.Complete();
        }

        public int RemoveRange(IEnumerable<Report> list)
        {
            var unitOfWork = new UnitOfWork(new FitnessTrackerDbContext());

            unitOfWork.Reports.RemoveRange(list);

            return unitOfWork.Complete();
        }

        public int Update(Report item)
        {
            var unitOfWork = new UnitOfWork(new FitnessTrackerDbContext());

            var report = unitOfWork.Reports.Get(item.Id);
            report.Date = item.Date;
            report.Weight = item.Weight;

            return unitOfWork.Complete();
        }
    }
}
