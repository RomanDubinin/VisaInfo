using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using VisaProject;

namespace DataLoad
{
    class Program
    {
        private static readonly string baseDirectory = "/root/VisaInfos";
        private static readonly IVisaResultLoader visaResultLoader = new VisaResultLoader();
        private static readonly StatementNumberGenerator statementNumberGenerator = new StatementNumberGenerator();
        private static readonly int dailyAmount = 16;
        private static readonly string logFile = "/root/VisaInfos/Log/Log.txt";
        private static readonly string dateFormat = "yyyy.MM.dd.hh.mm.ss";

        private static DataLoader dataLoader;

        static async Task Main(string[] args)
        {

            var expression = "* 2 * * *";
            var croneExpression = CronExpression.Parse(expression);
            dataLoader = new DataLoader(statementNumberGenerator, visaResultLoader);

            if (args.Length > 0 && args[0] == "--now")
            {
                Log("Starting update right now.");
                Log($"Next update will be in scheduled time {expression}");
                await UpdateAllData();
                Log("--");
            }
            else
            {
                Log("Hi!");
                Log($"Next update will be in scheduled time {expression}");
            }
            await RepeatDaily(async () => await UpdateAllData(), croneExpression);
        }

        private static async Task UpdateAllData()
        {
            var cities = new[] {"YEKA", "PETE", "MOSC", "KIEV"};
            var dateTo = DateTime.UtcNow.AddDays(-7);
            var dateFrom = dateTo.AddDays(-120);

            foreach (var city in cities)
            {
                Log($"Start load {city}");
                var fileName = Path.Join(baseDirectory, city, $"_temp-{city}.txt");
                CreateFile(fileName);

                try
                {
                    await UpdateCityData(dateFrom, dateTo, city, fileName);
                }
                catch (Exception e)
                {
                    Log(e.ToString());
                    throw;
                }

                var newFileName = Path.Join(baseDirectory, city, $"{city}_{DateTime.UtcNow.ToString(dateFormat)}.txt");
                File.Move(fileName, newFileName);
                Log($"End load {city}");
            }
        }

        private static async Task UpdateCityData(DateTime dateFrom, DateTime dateTo, string city, string fileName)
        {
            Action eachItemWait = () => Thread.Sleep(TimeSpan.FromSeconds(1));

            var visaInfos = dataLoader.GetVisaInfos(
                dateFrom,
                dateTo,
                city,
                dailyAmount,
                IsWorkingDay,
                eachItemWait);

            await foreach (var visaInfo in visaInfos)
            {
                WriteToFile(fileName, visaInfo);
            }
        }

        private static async Task RepeatDaily(Action action, CronExpression cronExpression)
        {
            var now = DateTime.UtcNow;
            while (true)
            {
                DateTime? nextUtc = cronExpression.GetNextOccurrence(now);
                var timeToStart = (nextUtc - now) ?? TimeSpan.Zero;
                Log($"Time to next start is {timeToStart}");
                await Task.Delay(timeToStart, CancellationToken.None);

                Log($"Action starts at {DateTime.UtcNow:hh:mm}");
                action();
                now = DateTime.UtcNow;
                Log($"Action ended at {now:hh:mm}");
            }
        }

        private static void CreateFile(string city)
        {
            using (File.Create(city))
            {
            }
        }

        private static void WriteToFile(string fileName, VisaInfo info)
        {
            File.AppendAllText(fileName, $"{info.ToString()}\n");
        }

        private static bool IsWorkingDay(DayOfWeek dayOfWeek)
        {
            return dayOfWeek != DayOfWeek.Saturday &&
                   dayOfWeek != DayOfWeek.Sunday;
        }

        private static void Log(string text)
        {
            File.AppendAllText(logFile, $"{DateTime.UtcNow.ToString(dateFormat)} - {text}\n");
        }
    }
}