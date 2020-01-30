using System;

namespace VisaProject
{
    public struct VisaStatisticItem
    {
        public VisaStatisticItem(DateTime date, int inServiceCount, int failureCount, int successCount)
        {
            Date = date;
            InServiceCount = inServiceCount;
            FailureCount = failureCount;
            SuccessCount = successCount;
        }

        public DateTime Date { get; }
        public int InServiceCount { get; }
        public int FailureCount { get; }
        public int SuccessCount { get; }
    }
}