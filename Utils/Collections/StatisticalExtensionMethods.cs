using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lospi.Utils.Collections
{
    /// <summary>
    /// Houses statistical extension methods for collections
    /// </summary>
    public static partial class ExtensionMethods
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

        /// <summary>
        /// Returns a random key with probability given by its Value.
        /// </summary>
        /// <typeparam networkName="To">Some key type</typeparam>
        /// <param networkName="dictionary">Extension method on dictionary</param>
        /// <returns>A random key</returns>
        public static T RandomKey<T>(this IDictionary<T, double> dictionary, double randomNumber)
        {
            foreach (T key in dictionary.Keys)
            {
                if (randomNumber > dictionary[key])
                {
                    randomNumber -= dictionary[key];
                }
                else
                {
                    return key;
                }
            }
            throw new ArgumentException("Values do not sum to one: " + dictionary.Sum(x => x.Value));
        }

    }
}
