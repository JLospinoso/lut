/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using Lospi.Utils.Generics;
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Lospi.Test.Utils.Generic
{
    [TestFixture]
    public class StatisticalExtensionMethodsTest
    {
        double[] values = { -4D, 4D, -4D, 4D };


        [Test]
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

            Assert.That(ctr, Is.EqualTo(6666).Within(5).Percent);
        }

        [Test]
        public void StandardDeviationTest()
        {
            ICollection<double> x = new List<double>(values);
            bool sample = false;
            double expected = 2F;
            double actual;
            actual = ExtensionMethods.StandardDeviation(x, sample);
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void VarianceTest()
        {
            ICollection<double> x = new List<double>(values);
            bool sample = false;
            double expected = 16F;
            double actual;
            actual = ExtensionMethods.Variance(x, sample);
            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
