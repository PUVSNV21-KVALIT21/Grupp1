using Microsoft.VisualStudio.TestTools.UnitTesting; // Use this to make it a test class
using Bogus;
using hakims_livs.Utils; // Faker library

namespace Hakims_Livs.tests.Data
{
    [TestClass]
    public class UnitTestExample
    {
        private readonly Faker _faker;

        public UnitTestExample()
        {
            _faker = new Faker();
        }

        [TestMethod]
        public void FormatPriceAssertPeriodToComma()
        {
            Assert.AreEqual("36,35",  FormatPrice.ToString((decimal) 36.35));
        }
        
        [TestMethod]
        public void FormatPriceAssertRemoveZerosAndCommaIfNotDecimal()
        {
            Assert.AreEqual("20",  FormatPrice.ToString((decimal) 20.0000));
        }
        
        [TestMethod]
        public void FormatPriceAssertRoundTwoDecimals()
        {
            Assert.AreEqual("20,23",  FormatPrice.ToString((decimal) 20.2345676456));
            Assert.AreEqual("0,01",  FormatPrice.ToString((decimal) 00.00634));
        }

    }
}
