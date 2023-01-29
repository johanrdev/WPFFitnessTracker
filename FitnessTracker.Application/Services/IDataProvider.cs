using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FitnessTracker.Application.Services
{
    public interface IDataProvider<T>
    {
        bool IsLoading { get; set; }
        ObservableCollection<T> Data { get; }

        IEnumerable<T> LoadData();
        void SetData(IEnumerable<T> data);
        int Add(T item);
        int RemoveRange(IEnumerable<T> item);
        int Update(T item);
    }
}
