using System;
using System.Linq;
using System.Threading.Tasks;
using DataLoad;
using Moq;
using NUnit.Framework;
using VisaProject;

namespace UnitTests
{
    public class DataLoaderTests
    {
        private Mock<IVisaResultLoader> visaResultLoader;
        private DataLoader dataLoader;
        private StatementNumberGenerator statementNumberGenerator;
        private readonly string city = "CITY";

        [SetUp]
        public void Setup()
        {
            visaResultLoader = new Mock<IVisaResultLoader>();
            visaResultLoader.Setup(x => x.LoadVisaResultByStatementNumber(It.IsAny<string>()))
                            .Returns(Task.FromResult(VisaResult.InService));
            statementNumberGenerator = new StatementNumberGenerator();
            dataLoader = new DataLoader(statementNumberGenerator, visaResultLoader.Object);
        }

        [Test]
        public async Task TestUpdateData3ItemsPerDay()
        {
            var dateFrom = new DateTime(2020, 4, 1);
            var dateTo = new DateTime(2020, 4, 1);

            var expectedData = new[]
            {
                new VisaInfo(city, VisaResult.InService, "CITY202004010001", new DateTime(2020, 4, 1)),
                new VisaInfo(city, VisaResult.InService, "CITY202004010002", new DateTime(2020, 4, 1)),
                new VisaInfo(city, VisaResult.InService, "CITY202004010003", new DateTime(2020, 4, 1)),
            };

            var actualData = await dataLoader
                                   .GetVisaInfos(dateFrom, dateTo, city, 3, dayOfWeek => true, () => { })
                                   .ToArrayAsync();

            Assert.That(actualData, Is.EqualTo(expectedData));
        }

        [Test]
        public async Task TestUpdateDataWithOnlySomeDaysOfWeek()
        {
            var dateFrom = new DateTime(2020, 4, 1);
            var dateTo = new DateTime(2020, 4, 7);

            bool IsWorkingDay(DayOfWeek dayOfWeek) =>
                dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Friday || dayOfWeek == DayOfWeek.Sunday;

            var expectedData = new[]
            {
                new VisaInfo(city, VisaResult.InService, "CITY202004030001", new DateTime(2020, 4, 3)),
                new VisaInfo(city, VisaResult.InService, "CITY202004050001", new DateTime(2020, 4, 5)),
                new VisaInfo(city, VisaResult.InService, "CITY202004070001", new DateTime(2020, 4, 7)),
            };

            var actualData = await dataLoader
                                   .GetVisaInfos(dateFrom, dateTo, city, 1, IsWorkingDay, () => { })
                                   .ToArrayAsync();

            Assert.That(actualData, Is.EqualTo(expectedData));
        }
    }
}