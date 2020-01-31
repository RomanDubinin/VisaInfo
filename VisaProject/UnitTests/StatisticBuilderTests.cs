using System;
using NUnit.Framework;
using UnitTests.Mock;
using VisaProject;

namespace UnitTests
{
    public class StatisticBuilderTests
    {
        public string City;
        public IRepository Repository;
        public StatisticBuilder StatisticBuilder;

        [SetUp]
        public void Setup()
        {
            City = "city";
            Repository = new TestRepository();
            StatisticBuilder = new StatisticBuilder();
        }

        [Test]
        public void TestGetStatisticByDays()
        {
            var date1 = new DateTime(2018, 04, 05);
            var date2 = new DateTime(2018, 04, 06);
            var visaInfos = new[]
            {
                new VisaInfo(City, VisaResult.None, "1", date1),
                new VisaInfo(City, VisaResult.Failure, "2", date1),
                new VisaInfo(City, VisaResult.None, "3", date1),
                new VisaInfo(City, VisaResult.Success, "4", date1),
                new VisaInfo(City, VisaResult.InService, "5", date1),
                new VisaInfo(City, VisaResult.InService, "6", date1),

                new VisaInfo(City, VisaResult.Success, "1", date2),
                new VisaInfo(City, VisaResult.InService, "2", date2),
                new VisaInfo(City, VisaResult.InService, "3", date2),
                new VisaInfo(City, VisaResult.InService, "4", date2),
                new VisaInfo(City, VisaResult.Success, "5", date2),
                new VisaInfo(City, VisaResult.Success, "6", date2),
                new VisaInfo(City, VisaResult.InService, "7", date2),
            };

            foreach (var visaInfo in visaInfos)
            {
                Repository.Write(visaInfo);
            }

            var expectedStatistic = new[]
            {
                new VisaStatisticItem(date1, 2, 1, 1),
                new VisaStatisticItem(date2, 4, 0, 3),
            };

            var actualStatistic = StatisticBuilder.BuildStatisticByDays(visaInfos);

            Assert.That(actualStatistic, Is.EqualTo(expectedStatistic));
        }
    }
}