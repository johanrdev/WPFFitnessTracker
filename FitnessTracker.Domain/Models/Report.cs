using Prism.Mvvm;
using System;

namespace FitnessTracker.Domain.Models
{
    public class Report : BindableBase
    {
        private int _id;
        private DateTime _date;
        private double _weight;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }
    }
}
