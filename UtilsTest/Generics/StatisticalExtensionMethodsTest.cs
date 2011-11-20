/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using Lospi.Utils.Generics;
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class StatisticalExtensionMethodsTest
    {
        readonly double[] _values = { -4D, 4D, -4D, 4D };


        [Test]
        public void RandomKeyTest()
        {
            var rng = new Random();
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

            Assert.That(ctr, Is.EqualTo(6666).Within(5).Percent);
        }

        [Test]
        public void StandardDeviationTest()
        {
            ICollection<double> collection = new List<double>(_values);
            const bool sample = false;
            const float expected = 2F;
            var actual = collection.StandardDeviation(sample);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void VarianceTest()
        {
            ICollection<double> x = new List<double>(_values);
            const bool sample = false;
            const double expected = 16F;
            var actual = x.Variance(sample);

            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
