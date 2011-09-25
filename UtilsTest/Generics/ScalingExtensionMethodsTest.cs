/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Lospi.Utils.Generics;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class ScalingExtensionMethodsTest
    {
        IList<double> list;
        double[] values = { -5F, -2F, 1F, 10F };

        [SetUp]
        public void SetUpTest()
        {
            list = new List<double>(values);
        }

        [TearDown]
        public void TearDownTest()
        {
            list = null;
        }

        [Test]
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
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

        [Test]
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
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

        [Test]
        public void RangeTest()
        {
            double lowerPercentile = 0F;
            double upperPercentile = 1F;
            double expected = 15F;
            double actual;
            actual = ExtensionMethods.Range(list, lowerPercentile, upperPercentile);
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
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
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

        [Test]
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
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

        [Test]
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
            foreach (double key in actual.Keys)
            {
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

        [Test]
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
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

    }
}
