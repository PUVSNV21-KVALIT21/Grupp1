using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hakims_livs.Data;                         

using Microsoft.VisualStudio.TestTools.UnitTesting; // Use this to make it a test class
using Bogus;                                        // Faker library

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
        public void UnitTest()
        {

        }

    }
}
