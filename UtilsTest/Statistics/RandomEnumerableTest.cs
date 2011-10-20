/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using NUnit.Framework;
using Lospi.Utils.Statistics;
using System.Collections.Generic;
using System.Linq;
using System;
using MathNet.Numerics.RandomSources;

namespace Lospi.Test.Utils.Statistics
{
    [TestFixture]
    public class RandomEnumerableTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomEnumerable_ProducesError_ForIllegalArgumentsOnRanges()
        {
            var randomSource = new MersenneTwisterRandomSource();
            Lospi.Utils.Statistics.RandomEnumerable.Range(11, -1, randomSource);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomEnumerable_ProducesError_ForIllegalArgumentsOnFroms()
        {
            var randomSource = new MersenneTwisterRandomSource();
            Lospi.Utils.Statistics.RandomEnumerable.From(-4, -5, randomSource);
        }


        [TestCase(0, 10)]
        [TestCase(-13, 7)]
        [TestCase(3, 3)]
        public void RandomEnumerable_Produces_RangesContainingWholeEnumeration(int start, int end)
        {
            var randomSource = new MersenneTwisterRandomSource();
            int trials = 1000;

            while (trials-- > 0)
            {
                var result = Lospi.Utils.Statistics.RandomEnumerable.Range(0, 10, randomSource);
                for(int i=0; i<10; i++)
                {
                    Assert.That(result, Contains.Item(i));
                }
            }
        }

        [Test]
        public void RandomEnumerable_Produces_RangesWithUniformDistributions()
        {
            var randomSource = new MersenneTwisterRandomSource();
            double trials = 100000;

            var zeroFrequency = Enumerable.Range(0, 10).ToDictionary(x => x, x => 0);

            var fiveFrequency = Enumerable.Range(0, 10).ToDictionary(x => x, x => 0);

            var nineFrequency = Enumerable.Range(0, 10).ToDictionary(x => x, x => 0);

            for (int count = 0; count < trials; count++ )
            {
                var result = Lospi.Utils.Statistics.RandomEnumerable.Range(0, 10, randomSource);

                zeroFrequency[result[0]]++;

                fiveFrequency[result[5]]++;

                nineFrequency[result[9]]++;
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.That(zeroFrequency[i], Is.EqualTo(trials / 10D).Within(10).Percent);
                Assert.That(fiveFrequency[i], Is.EqualTo(trials / 10D).Within(10).Percent);
                Assert.That(nineFrequency[i], Is.EqualTo(trials / 10D).Within(10).Percent);
            }
        }
    }
}
