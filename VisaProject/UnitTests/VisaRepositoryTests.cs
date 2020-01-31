using System;
using System.IO;
using DataContainer;
using NUnit.Framework;
using VisaProject;
using Moq;

namespace UnitTests
{
    public class VisaRepositoryTests
    {
        private static readonly string city = "City";
        private readonly string fullFileName = Path.Combine(Directory.GetCurrentDirectory(), city, "testInfos.txt");
        private Mock<IFileNameFinder> fileNameFinder { get; set; }
        private VisaRepository VisaRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(city);
            fileNameFinder = new Mock<IFileNameFinder>();
            fileNameFinder.Setup(x => x.FindName(city))
                          .Returns(fullFileName);
            VisaRepository = new VisaRepository(fileNameFinder.Object);
        }

        [Test]
        public void TestRewriteOneInfo()
        {
            var visaDate = DateTime.Today;
            var expectedInfos = new[]
            {
                new VisaInfo(city, VisaResult.None, "number", visaDate)
            };
            foreach (var visaInfo in expectedInfos)
            {
                WriteToFile(visaInfo);
            }
            var filter = new VisaInfoFilter(city);
            var actualInfos = VisaRepository.Read(filter);

            Assert.That(actualInfos, Is.EqualTo(expectedInfos));
        }

        [Test]
        public void TestRewriteManyInfos()
        {
            var visaDate = DateTime.Today;
            var expectedInfos = new[]
            {
                new VisaInfo(city, VisaResult.None, "number", visaDate),
                new VisaInfo(city, VisaResult.Failure, "number2", visaDate),
                new VisaInfo(city, VisaResult.Success, "number3", visaDate),
                new VisaInfo(city, VisaResult.InService, "number4", visaDate)
            };
            foreach (var visaInfo in expectedInfos)
            {
                WriteToFile(visaInfo);
            }

            var filter = new VisaInfoFilter(city);
            var actualInfos = VisaRepository.Read(filter);

            Assert.That(actualInfos, Is.EqualTo(expectedInfos));
        }

        [Test]
        public void TestWriteOneByOneManyInfos()
        {
            var visaDate = DateTime.Today;
            var expectedInfos = new[]
            {
                new VisaInfo(city, VisaResult.None, "number", visaDate),
                new VisaInfo(city, VisaResult.Failure, "number2", visaDate),
                new VisaInfo(city, VisaResult.Success, "number3", visaDate),
                new VisaInfo(city, VisaResult.InService, "number4", visaDate)
            };
            foreach (var expectedInfo in expectedInfos)
            {
                WriteToFile(expectedInfo);
            }
            var filter = new VisaInfoFilter(city);
            var actualInfos = VisaRepository.Read(filter);

            Assert.That(actualInfos, Is.EqualTo(expectedInfos));
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(fullFileName);
            Directory.Delete(city);
        }

        private void WriteToFile(VisaInfo info)
        {
            File.AppendAllText(fullFileName, $"{info.ToString()}\n");
        }
    }
}