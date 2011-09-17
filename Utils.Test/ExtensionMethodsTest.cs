using Lospi.Utils.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Lospi.Utils;
using System.Linq;

namespace Lospi.Utils.Test
{
    
    
    /// <summary>
    ///This is a test class for ExtensionMethodsTest and is intended
    ///to contain all ExtensionMethodsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExtensionMethodsTest
    {
        double[] values = { -5F, -2F, 1F, 10F };
        IList<double> list;

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

        [TestInitialize()]
        public void InitializeValues()
        {
            list = new List<double>(values);
        }

        /// <summary>
        ///A test for Normalize
        ///</summary>
        [TestMethod()]
        public void NormalizeTest()
        {
            double mean = 0F;
            double stdev = 1F;
            IDictionary<double, double> actual;
            actual = ExtensionMethods.Normalize(list, mean, stdev);
            IDictionary<double, double> expected = new Dictionary<double, double>
            {
                { -5F, -1.150349F },
                { -2F, -0.3186394F },
                {  1F,  0.3186394F },
                { 10F,  1.150349F }
            };
            foreach (double key in expected.Keys)
            {
                Assert.IsTrue(Math.Abs(actual[key] - expected[key]) < .00001D);
            }
        }

        /// <summary>
        ///A test for Percentile
        ///</summary>
        [TestMethod()]
        public void PercentileTest()
        {
            bool midpoint = true;
            IDictionary<double, double> expected = new Dictionary<double, double>
            {
                { -5F, 0.5F / 4F },
                { -2F, 1.5F / 4F },
                {  1F, 2.5F / 4F },
                { 10F, 3.5F / 4F }
            };
            IDictionary<double, double> actual;
            actual = ExtensionMethods.Percentile(list, midpoint);
            foreach (double key in expected.Keys)
            {
                Assert.IsTrue(Math.Abs(actual[key] - expected[key]) < .00001D);
            }
        }

        /// <summary>
        ///A test for Range
        ///</summary>
        [TestMethod()]
        public void RangeTest()
        {
            double lowerPercentile = 0F;
            double upperPercentile = 1F;
            double expected = 15F;
            double actual;
            actual = ExtensionMethods.Range(list, lowerPercentile, upperPercentile);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Rank
        ///</summary>
        [TestMethod()]
        public void RankTest()
        {
            IDictionary<double, double> expected = new Dictionary<double, double>
            {
                { -5F, 1F },
                { -2F, 2F },
                {  1F, 3F },
                { 10F, 4F }
            };
            IDictionary<double, double> actual;
            actual = ExtensionMethods.Rank(list);
            foreach (double key in expected.Keys)
            {
                Assert.IsTrue(Math.Abs(actual[key] - expected[key]) < .00001D);
            }
        }

        /// <summary>
        ///A test for Scale
        ///</summary>
        [TestMethod()]
        public void ScaleTest()
        {
            double lower = 0F;
            double upper = 1F;
            IDictionary<double, double> expected = new Dictionary<double, double>
            {
                { -5F, 0F },
                { -2F, 0.2F },
                {  1F, 0.4F },
                { 10F, 1F }
            };
            IDictionary<double, double> actual;
            actual = ExtensionMethods.Scale(list, lower, upper);
            foreach (double key in expected.Keys)
            {
                Assert.IsTrue(Math.Abs(actual[key] - expected[key]) < .00001D);
            }
        }

        [TestMethod()]
        public void SortOnKeysTest()
        {
            IDictionary<double, double> dictionary = new Dictionary<double, double>
            {
                { -5F, 1F },
                { -2F, 0.2F },
                { -7F, 0.4F },
                { 10F, 1F }
            };
            SortedDictionary<double, double> expected = new SortedDictionary<double, double>
            {
                { -5F, 1F },
                { -2F, 0.2F },
                { -7F, 0.4F },
                { 10F, 1F }
            };
            SortedDictionary<double, double> actual;
            actual = ExtensionMethods.SortOnKeys(dictionary);
            foreach(double key in actual.Keys)
            {
                Assert.IsTrue(Math.Abs(actual[key] - expected[key]) < .00001D);
            }
        }

        /// <summary>
        ///A test for Standardize
        ///</summary>
        [TestMethod()]
        public void StandardizeTest()
        {
            IDictionary<double, double> expected = new SortedDictionary<double, double>
            {
                { -5F, -1.069044968F },
                { -2F, -0.534522484F },
                {  1F,  0F },
                { 10F,  1.603567451F }
            };
            IDictionary<double, double> actual;
            actual = ExtensionMethods.Standardize(list);
            foreach (double key in expected.Keys)
            {
                Assert.IsTrue(Math.Abs(actual[key] - expected[key]) < .00001D);
            }
        }

        class TestCopyValue : IDeepCopyable<TestCopyValue>
        {
            public string Value { get; set; }

            public TestCopyValue DeepCopy()
            {
                return new TestCopyValue { Value = this.Value };
            }
        }

        [TestMethod()]
        public void DeepMemberwiseCopyTest()
        {
            IList<TestCopyValue> baseList = new List<TestCopyValue>();
            int count = 10;

            for (int i = 0; i < count; i++)
            {
                baseList.Add(new TestCopyValue { Value = i.ToString() });
            }

            IList<TestCopyValue> deepCopy = baseList.DeepMemberwiseCopy().ToList();

            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(baseList[i].Value.Equals(deepCopy[i].Value));
            }

            for (int i = 0; i < count; i++)
            {
                baseList[i].Value = "CLEARED";
            }

            for (int i = 0; i < count; i++)
            {
                Assert.IsFalse(baseList[i].Value.Equals(deepCopy[i].Value));
            }
        }
    }
}
