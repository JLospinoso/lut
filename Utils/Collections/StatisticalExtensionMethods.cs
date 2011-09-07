using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lospi.Utils.Collections
{
    /// <summary>
    /// Houses statistical extension methods for collections
    /// </summary>
    public static class StatisticalExtensionMethods
    {
        /// <summary>
        /// Calculates the standard deviation of a collection of doubles
        /// </summary>
        /// <param name="x">A collection of doubles</param>
        /// <param name="sample">True if sample standard deviation is to be calculated, false if population</param>
        /// <returns>The standard deviation of x</returns>
        public static Double StandardDeviation(this ICollection<Double> x, bool sample=true)
        {
            Double mean = x.Average();
            Double denominator = x.Count;
            if (sample)
            {
                denominator--;
            }
            return Math.Sqrt(x.Select(val => Math.Pow(val - mean, 2)).Sum())/denominator;
        }

        /// <summary>
        /// Calculates the variance of a collection of doubles
        /// </summary>
        /// <param name="x">A collection of doubles</param>
        /// <param name="sample">True if sample variance is to be calculated, false if population</param>
        /// <returns>The variance of x</returns>
        public static Double Variance(this ICollection<Double> x, bool sample = true)
        {
            Double mean = x.Average();
            Double denominator = x.Count;
            if (sample)
            {
                denominator--;
            }
            return x.Select(val => Math.Pow(val - mean, 2)).Sum() / denominator;
        }
    }
}
