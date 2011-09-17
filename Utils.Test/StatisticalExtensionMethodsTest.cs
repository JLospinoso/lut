/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using Lospi.Utils.Generics;
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

        [TestMethod()]
        public void RandomKeyTest()
        {
            Random rng = new Random();
            IDictionary<String, Double> dictionary = new Dictionary<string, double>();

            dictionary["one"] = 2.0 / 3.0;
            dictionary["two"] = 1.0 / 6.0;
            dictionary["thr"] = 1.0 / 6.0;
            int i = 10000;
            int ctr = 0;
            while (i-- > 0)
            {
                if (dictionary.RandomKey(rng.NextDouble()) == "one")
                {
                    ctr++;
                }
            }
            // This is probabilistic, but the range covers most of the probability mass of ctr.
            Assert.IsTrue((ctr > 6400) & (ctr < 6800));
        }

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
            actual = ExtensionMethods.StandardDeviation(x, sample);
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
            actual = ExtensionMethods.Variance(x, sample);
            Assert.AreEqual(expected, actual);
        }
    }
}
