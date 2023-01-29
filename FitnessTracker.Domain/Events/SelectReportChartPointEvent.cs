using FitnessTracker.Domain.Models;
using Prism.Events;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    public class SelectReportChartPointEvent : PubSubEvent<Report>
    {
    }
}