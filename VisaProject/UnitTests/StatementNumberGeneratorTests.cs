using System;
using DataLoad;
using NUnit.Framework;

namespace UnitTests
{
    public class StatementNumberGeneratorTests
    {
        private StatementNumberGenerator StatementNumberGenerator { get; set; }

        [SetUp]
        public void Setup()
        {
            StatementNumberGenerator = new StatementNumberGenerator();
        }

        [Test]
        public void TestCreateFromStringWith3PaddingZeros()
        {
            var city = "YEKA";
            var date = new DateTime(2017, 04, 23);
            var number = 1;
            var expectedStatementNumber = "YEKA201704230001";

            var actualStatementNumber = StatementNumberGenerator.Generate(city, date, number);

            Assert.That(actualStatementNumber, Is.EqualTo(expectedStatementNumber));
        }

        [Test]
        public void TestCreateFromStringWith1PaddingZero()
        {
            var city = "YEKA";
            var date = new DateTime(2017, 04, 23);
            var number = 123;
            var expectedStatementNumber = "YEKA201704230123";

            var actualStatementNumber = StatementNumberGenerator.Generate(city, date, number);

            Assert.That(actualStatementNumber, Is.EqualTo(expectedStatementNumber));
        }

    }
}