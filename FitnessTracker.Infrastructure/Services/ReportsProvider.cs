﻿using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Models;
using FitnessTracker.Infrastructure.Repository;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

        public ObservableCollection<Report> Data { get; private set; }

        public ReportsProvider()
        {
            Data = new ObservableCollection<Report>();
        }

        public IEnumerable<Report> LoadData()
        {
            var unitOfWork = new UnitOfWork(new FitnessTrackerDbContext());

            Debug.WriteLine($"Is Loading: {IsLoading}");

            return unitOfWork.Reports.GetAll();
        }

        public void SetData(IEnumerable<Report> data)
        {
            Data.Clear();

            foreach (var item in data) Data.Add(item);
        }
    }
}
