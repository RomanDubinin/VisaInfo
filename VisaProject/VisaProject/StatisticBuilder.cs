using System;
using System.Linq;

namespace VisaProject
{
    public class StatisticBuilder
    {
        public VisaStatisticItem[] BuildStatisticByDays(VisaInfo[] visaInfos)
        {
            return visaInfos
                   .GroupBy(x => x.StatementDate)
                   .Select(VisaInfoGroupToVisaStatisticItem)
                   .ToArray();
        }

        private VisaStatisticItem VisaInfoGroupToVisaStatisticItem(IGrouping<DateTime, VisaInfo> visaInfoGroup)
        {
            return new VisaStatisticItem(
                visaInfoGroup.Key,
                visaInfoGroup.Count(y => y.Result == VisaResult.InService),
                visaInfoGroup.Count(y => y.Result == VisaResult.Failure),
                visaInfoGroup.Count(y => y.Result == VisaResult.Success));
        }
    }
}