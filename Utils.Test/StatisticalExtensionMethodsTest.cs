using Lospi.Utils.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Lospi.Utils.Test
{
    
    
    /// <summary>
    ///This is a test class for StatisticalExtensionMethodsTest and is intended
    ///to contain all StatisticalExtensionMethodsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatisticalExtensionMethodsTest
    {
        double[] values = { -4D, 4D, -4D, 4D };

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for StandardDeviation
        ///</summary>
        [TestMethod()]
        public void StandardDeviationTest()
        {
            ICollection<double> x = new List<double>(values);
            bool sample = false;
            double expected = 2F;
            double actual;
            actual = StatisticalExtensionMethods.StandardDeviation(x, sample);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Variance
        ///</summary>
        [TestMethod()]
        public void VarianceTest()
        {
            ICollection<double> x = new List<double>(values);
            bool sample = false;
            double expected = 16F;
            double actual;
            actual = StatisticalExtensionMethods.Variance(x, sample);
            Assert.AreEqual(expected, actual);
        }
    }
}
