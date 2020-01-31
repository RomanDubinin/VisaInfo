using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisaProject;

namespace DataLoad
{
    public class DataLoader
    {
        private readonly StatementNumberGenerator statementNumberGenerator;
        private readonly IVisaResultLoader visaResultLoader;

        public DataLoader(StatementNumberGenerator statementNumberGenerator, IVisaResultLoader visaResultLoader)
        {
            this.statementNumberGenerator = statementNumberGenerator;
            this.visaResultLoader = visaResultLoader;
        }

        public async IAsyncEnumerable<VisaInfo> GetVisaInfos(
            DateTime dateFrom,
            DateTime dateTo,
            string city,
            int dailyAmount,
            Func<DayOfWeek, bool> isWorkingDay,
            Action eachItemWait)
        {
            var currentDate = dateFrom;

            while (currentDate <= dateTo)
            {
                if (!isWorkingDay(currentDate.DayOfWeek))
                {
                    currentDate = currentDate.AddDays(1);
                    continue;
                }

                for (var i = 1; i <= dailyAmount; i++)
                {
                    var statementNumber = statementNumberGenerator.Generate(city, currentDate, i);
                    var visaResult = await visaResultLoader.LoadVisaResultByStatementNumber(statementNumber);
                    var visaInfo = new VisaInfo(city, visaResult, statementNumber, currentDate);
                    yield return visaInfo;
                    eachItemWait();
                }

                currentDate = currentDate.AddDays(1);
            }
        }
    }
}