using System;
using System.IO;
using DataContainer;
using NUnit.Framework;
using UnitTests.Mock;
using VisaProject;

namespace UnitTests
{
    public class VisaRepositoryTests
    {
        private readonly string fileName = "testInfos.txt";
        private readonly VisaInfoFilter filter = new VisaInfoFilter("");
        private IFileNameFinder fileNameFinder { get; set; }
        private VisaRepository VisaRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            fileNameFinder = new TestFileNameFinder(fileName);
            VisaRepository = new VisaRepository(fileNameFinder);
        }

        [Test]
        public void TestRewriteOneInfo()
        {
            var visaDate = DateTime.Today;
            var expectedInfos = new[]
            {
                new VisaInfo(VisaResult.None, "number", visaDate)
            };
            foreach (var visaInfo in expectedInfos)
            {
                VisaRepository.Write(visaInfo);
            }
            var actualInfos = VisaRepository.Read(filter);

            Assert.That(actualInfos, Is.EqualTo(expectedInfos));
        }

        [Test]
        public void TestRewriteManyInfos()
        {
            var visaDate = DateTime.Today;
            var expectedInfos = new[]
            {
                new VisaInfo(VisaResult.None, "number", visaDate),
                new VisaInfo(VisaResult.Failure, "number2", visaDate),
                new VisaInfo(VisaResult.Success, "number3", visaDate),
                new VisaInfo(VisaResult.InService, "number4", visaDate)
            };
            foreach (var visaInfo in expectedInfos)
            {
                VisaRepository.Write(visaInfo);
            }
            var actualInfos = VisaRepository.Read(filter);

            Assert.That(actualInfos, Is.EqualTo(expectedInfos));
        }

        [Test]
        public void TestWriteOneByOneManyInfos()
        {
            var visaDate = DateTime.Today;
            var expectedInfos = new[]
            {
                new VisaInfo(VisaResult.None, "number", visaDate),
                new VisaInfo(VisaResult.Failure, "number2", visaDate),
                new VisaInfo(VisaResult.Success, "number3", visaDate),
                new VisaInfo(VisaResult.InService, "number4", visaDate)
            };
            foreach (var expectedInfo in expectedInfos)
            {
                VisaRepository.Write(expectedInfo);
            }
            var actualInfos = VisaRepository.Read(filter);

            Assert.That(actualInfos, Is.EqualTo(expectedInfos));
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(fileName);
        }
    }
}