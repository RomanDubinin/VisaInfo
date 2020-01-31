﻿using System;
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
        public IRepository Repository;
        public Mock<IVisaResultLoader> VisaResultLoader;
        public DataUpdater DataUpdater;
        public StatementNumberGenerator StatementNumberGenerator;
        public readonly string City = "CITY";
        public readonly VisaInfoFilter EmptyFilter = new VisaInfoFilter();

        [SetUp]
        public void Setup()
        {
            Repository = new TestRepository();
            VisaResultLoader = new Mock<IVisaResultLoader>();
            VisaResultLoader.Setup(x => x.LoadVisaResultByStatementNumber(It.IsAny<string>()))
                            .Returns(Task.FromResult(VisaResult.InService));
            StatementNumberGenerator = new StatementNumberGenerator();
            DataUpdater = new DataUpdater(StatementNumberGenerator, VisaResultLoader.Object, Repository);
        }

        [Test]
        public void TestUpdateData3ItemsPerDay()
        {
            var dateFrom = new DateTime(2020, 4, 1);
            var dateTo = new DateTime(2020, 4, 1);

            var expectedData = new[]
            {
                new VisaInfo(City, VisaResult.InService, "CITY202004010001", new DateTime(2020, 4, 1)),
                new VisaInfo(City, VisaResult.InService, "CITY202004010002", new DateTime(2020, 4, 1)),
                new VisaInfo(City, VisaResult.InService, "CITY202004010003", new DateTime(2020, 4, 1)),
            };

            DataUpdater.UpdateData(dateFrom, dateTo, City, 3, dayOfWeek => true, () => { });

            var actualData = Repository.Read(EmptyFilter);

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
                new VisaInfo(City, VisaResult.InService, "CITY202004030001", new DateTime(2020, 4, 3)),
                new VisaInfo(City, VisaResult.InService, "CITY202004050001", new DateTime(2020, 4, 5)),
                new VisaInfo(City, VisaResult.InService, "CITY202004070001", new DateTime(2020, 4, 7)),
            };

            DataUpdater.UpdateData(dateFrom, dateTo, City, 1, IsWorkingDay, () => { });

            var actualData = Repository.Read(EmptyFilter);

            Assert.That(actualData, Is.EqualTo(expectedData));
        }
    }
}