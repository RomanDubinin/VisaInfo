using System;
using DataContainer;
using NUnit.Framework;
using UnitTests.Mock;
using VisaProject;

namespace UnitTests
{
    public class ActualityDateProviderTests
    {
        private IActualityDateProvider actualityDateProvider;
        private IFileNameFinder fileNameFinder;

        [Test]
        public void Test()
        {
            var expectedDate = new DateTime(2019, 03, 04, 03, 02, 01);
            fileNameFinder = new TestFileNameFinder("/dir/", $"file_{expectedDate:yyyy.MM.dd.hh.mm.ss}.txt");
            actualityDateProvider = new ActualityDateProvider(fileNameFinder);
            var actualDate = actualityDateProvider.GetActualityDate("city");
            Assert.That(actualDate, Is.EqualTo(expectedDate));
        }
    }
}