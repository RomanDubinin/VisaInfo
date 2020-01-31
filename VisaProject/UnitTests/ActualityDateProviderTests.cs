using System;
using DataContainer;
using Moq;
using NUnit.Framework;
using VisaProject;

namespace UnitTests
{
    public class ActualityDateProviderTests
    {
        private IActualityDateProvider actualityDateProvider;

        [Test]
        public void TestGetActualDate()
        {
            var expectedDate = new DateTime(2019, 03, 04, 03, 02, 01);
            var fileNameFinder = new Mock<IFileNameFinder>();
            fileNameFinder
                .Setup(x => x.FindName("city"))
                .Returns($"/dir/city/file_{expectedDate:yyyy.MM.dd.hh.mm.ss}.txt");

            actualityDateProvider = new ActualityDateProvider(fileNameFinder.Object);
            var actualDate = actualityDateProvider.GetActualityDate("city");
            Assert.That(actualDate, Is.EqualTo(expectedDate));
        }
    }
}