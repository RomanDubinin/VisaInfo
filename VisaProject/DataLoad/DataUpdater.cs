using System;
using System.Threading.Tasks;
using VisaProject;

namespace DataLoad
{
    public class DataUpdater
    {
        private readonly StatementNumberGenerator statementNumberGenerator;
        private readonly IVisaResultLoader visaResultLoader;
        private readonly IRepository visaRepository;

        public DataUpdater(StatementNumberGenerator statementNumberGenerator, IVisaResultLoader visaResultLoader, IRepository visaRepository)
        {
            this.statementNumberGenerator = statementNumberGenerator;
            this.visaResultLoader = visaResultLoader;
            this.visaRepository = visaRepository;
        }

        public async Task UpdateData(
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

                for (int i = 1; i <= dailyAmount; i++)
                {
                    var statementNumber = statementNumberGenerator.Generate(city, currentDate, i);
                    Console.WriteLine(statementNumber);
                    var visaResult = await visaResultLoader.LoadVisaResultByStatementNumber(statementNumber);
                    var visaInfo = new VisaInfo(visaResult, statementNumber, currentDate);
                    visaRepository.Write(visaInfo);
                    eachItemWait();
                }

                currentDate = currentDate.AddDays(1);
            }
        }
    }
}