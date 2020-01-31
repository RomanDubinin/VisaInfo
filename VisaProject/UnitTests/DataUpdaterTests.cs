using System;
using System.Threading.Tasks;
using DataLoad;
using Moq;
using NUnit.Framework;
using UnitTests.Mock;
using VisaProject;

namespace UnitTests
{
    public class DataUpdaterTests
    {
        private IRepository repository;
        private Mock<IVisaResultLoader> visaResultLoader;
        private DataUpdater dataUpdater;
        private StatementNumberGenerator statementNumberGenerator;
        private readonly string city = "CITY";
        private readonly VisaInfoFilter emptyFilter = new VisaInfoFilter();

        [SetUp]
        public void Setup()
        {
            repository = new TestRepository();
            visaResultLoader = new Mock<IVisaResultLoader>();
            visaResultLoader.Setup(x => x.LoadVisaResultByStatementNumber(It.IsAny<string>()))
                            .Returns(Task.FromResult(VisaResult.InService));
            statementNumberGenerator = new StatementNumberGenerator();
            dataUpdater = new DataUpdater(statementNumberGenerator, visaResultLoader.Object, repository);
        }

        [Test]
        public void TestUpdateData3ItemsPerDay()
        {
            var dateFrom = new DateTime(2020, 4, 1);
            var dateTo = new DateTime(2020, 4, 1);

            var expectedData = new[]
            {
                new VisaInfo(city, VisaResult.InService, "CITY202004010001", new DateTime(2020, 4, 1)),
                new VisaInfo(city, VisaResult.InService, "CITY202004010002", new DateTime(2020, 4, 1)),
                new VisaInfo(city, VisaResult.InService, "CITY202004010003", new DateTime(2020, 4, 1)),
            };

            dataUpdater.UpdateData(dateFrom, dateTo, city, 3, dayOfWeek => true, () => { });

            var actualData = repository.Read(emptyFilter);

            Assert.That(actualData, Is.EqualTo(expectedData));
        }

        [Test]
        public void TestUpdateDataWithOnlySomeDaysOfWeek()
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

            dataUpdater.UpdateData(dateFrom, dateTo, city, 1, IsWorkingDay, () => { });

            var actualData = repository.Read(emptyFilter);

            Assert.That(actualData, Is.EqualTo(expectedData));
        }
    }
}