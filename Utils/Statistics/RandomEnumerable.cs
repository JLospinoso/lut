﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.RandomSources;

namespace Lospi.Utils.Statistics
{
    public static class RandomEnumerable
    {
        /// <summary>
        /// Produces a contiguous sequence if integer values
        /// </summary>
        /// <param name="start">Start value, inclusive</param>
        /// <param name="end">Ending value, inclusive</param>
        /// <param name="random">A random source</param>
        /// <returns></returns>
        public static IList<int> From(int start, int end, RandomSource random)
        {
            if (end < start)
            {
                var message = String.Format("Start {0} cannot be less than end {1}.", start, end);
                throw new ArgumentException(message);
            }

            return Range(start, end - start, random);
        }

        /// <summary>
        /// Produces a contiguous sequence if integer values
        /// </summary>
        /// <param name="start">Start value, inclusive</param>
        /// <param name="end">Number of values to produce</param>
        /// <param name="random">A random source</param>
        /// <returns></returns>
        public static IList<int> Range(int start, int count, RandomSource random)
        {
            if (count < 0)
            {
                var message = String.Format("Count {0} cannot be less than 0.", count);
                throw new ArgumentException(message);
            }

            var result = Enumerable.Range(start, count).ToList();

            for (int i = 0; i < count; i++)
            {
                int newPosition = random.Next(0, count);

                var displacedValue = result[newPosition];

                result[newPosition] = result[i];

                result[i] = displacedValue;
            }

            for (int i = 0; i < count; i++)
            {
                int newPosition = random.Next(0, count);

                var displacedValue = result[newPosition];

                result[newPosition] = result[i];

                result[i] = displacedValue;
            }

            return result;
        }
    }
}
