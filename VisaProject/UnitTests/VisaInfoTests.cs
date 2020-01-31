using System;
using NUnit.Framework;
using VisaProject;

namespace UnitTests
{

    public class VisaInfoTests
    {
        [Test]
        public void TestCreateFromString()
        {
            var date = new DateTime(2017, 04, 23);

            var expectedInfo = new VisaInfo("City", VisaResult.Success, "number", date);
            var stringInfo = "City 2017.04.23 number Success";
            var actualInfo = new VisaInfo(stringInfo);

            Assert.That(actualInfo, Is.EqualTo(expectedInfo));
        }
    }
}