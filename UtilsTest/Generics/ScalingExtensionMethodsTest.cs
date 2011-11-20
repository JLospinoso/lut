/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System.Collections.Generic;
using Lospi.Utils.Generics;
using NUnit.Framework;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class ScalingExtensionMethodsTest
    {
        IList<double> _list;
        readonly double[] _values = { -5F, -2F, 1F, 10F };

        [SetUp]
        public void SetUpTest()
        {
            _list = new List<double>(_values);
        }

        [TearDown]
        public void TearDownTest()
        {
            _list = null;
        }

        [Test]
        public void NormalizeTest()
        {
            var actual = _list.Normalize();

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
            const bool midpoint = true;
            IDictionary<double, double> expected = new Dictionary<double, double>
            {
                { -5F, 0.5F / 4F },
                { -2F, 1.5F / 4F },
                {  1F, 2.5F / 4F },
                { 10F, 3.5F / 4F }
            };
            IDictionary<double, double> actual = _list.Percentile(midpoint);

            foreach (double key in expected.Keys)
            {
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

        [Test]
        public void RangeTest()
        {
            const double expected = 15F;

            var actual = _list.Range();

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
            IDictionary<double, double> actual = _list.Rank();

            foreach (var key in expected.Keys)
            {
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

        [Test]
        public void ScaleTest()
        {
            IDictionary<double, double> expected = new Dictionary<double, double>
            {
                { -5F, 0F },
                { -2F, 0.2F },
                {  1F, 0.4F },
                { 10F, 1F }
            };
            IDictionary<double, double> actual = _list.Scale();

            foreach (var key in expected.Keys)
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

            var expected = new SortedDictionary<double, double>
            {
                { -5F, 1F },
                { -2F, 0.2F },
                { -7F, 0.4F },
                { 10F, 1F }
            };

            SortedDictionary<double, double> actual = dictionary.SortOnKeys();
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
            IDictionary<double, double> actual = _list.Standardize();

            foreach (double key in expected.Keys)
            {
                Assert.That(actual[key], Is.EqualTo(expected[key]).Within(1).Percent);
            }
        }

    }
}
