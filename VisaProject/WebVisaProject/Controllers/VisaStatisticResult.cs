using System;
using VisaProject;

namespace WebVisaProject.Controllers
{
    public struct VisaStatisticResult
    {
        public VisaStatisticResult(DateTime actualDate, VisaStatisticItem[] statisticItems)
        {
            ActualDate = actualDate;
            StatisticItems = statisticItems;
        }

        public DateTime ActualDate { get; }
        public VisaStatisticItem[] StatisticItems { get; }
    }
}