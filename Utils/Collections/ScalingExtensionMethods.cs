using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;

namespace Lospi.Utils.Collections
{
    public static class ExtensionMethods
    {
        /// <summary>
        ///  Scales a list onto a new interval
        /// </summary>
        /// <param name="list"></param>
        /// <param name="lower">Lower limit of new interval</param>
        /// <param name="upper">Upper limit of new interval</param>
        /// <returns></returns>
        public static IDictionary<Double, Double> Scale(this IList<Double> list, double lower = 0, double upper = 1)
        {
            Double max = list.Max();
            Double min = list.Min();
            Double avg = list.Sum() / list.Count;
            return list.ToDictionary(x => x, x => (upper - lower) * ((x - min) / (max - min)) + lower);
        }

        /// <summary>
        /// Standardizes a list by subtracting its mean and dividing by its standard deviation to yield
        /// a new list with mean zero and standard deviation one.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IDictionary<Double, Double> Standardize(this IList<Double> list)
        {
            Double avg = list.Sum() / list.Count;
            Double var = list.Select(x => Math.Pow(x - avg, 2)).Sum() / list.Count;
            Double sd = Math.Pow(var, .5);
            return list.ToDictionary(x => x, x => (x - avg) / sd);
        }

        /// <summary>
        /// Calculates the percentile for each element
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IDictionary<Double, Double> Percentile(this IList<Double> list, bool midpoint = false)
        {
            double denominator;
            double numeratorAdjustment;
            if (midpoint)
            {
                denominator = list.Count;
                numeratorAdjustment = 0.5;
            }
            else
            {
                denominator = list.Count - 1;
                numeratorAdjustment = 0;
            }
            return list.ToDictionary(x => x, x => (list.Count(y => x > y) + numeratorAdjustment) / denominator);
        }

        /// <summary>
        /// Ranks a list of Doubles.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>A dictionary with Key = original value, Value = rank in the list from 1 to ..., where 1 is the lowest value.</returns>
        public static IDictionary<Double, Double> Rank(this IList<Double> list)
        {
            IEnumerable<Double> uniqueValues = list.Distinct();
            return list.ToDictionary(x => x, x => (Double) uniqueValues.Count(y => x >= y));
        }

        /// <summary>
        /// "Curves" the list by mapping it onto a standard normal distribution
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IDictionary<Double, Double> Normalize(this IList<Double> list, double mean=0, double stdev=1)
        {
            NormalDistribution normal = new NormalDistribution(mean, stdev);
            return list.Percentile(true).ToDictionary( x => x.Key, x => normal.InverseCumulativeDistribution(x.Value) );
        }

        /// <summary>
        /// Returns the inter-percentile range of a list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="lowerPercentile">Lower percentile</param>
        /// <param name="upperPercentile">Upper percentile</param>
        /// <returns></returns>
        public static Double Range(this IList<Double> list, double lowerPercentile = 0, double upperPercentile = 1)
        {
            IDictionary<Double, Double> lookup = list.Percentile(false);
            Double lowerValue = lookup.Where(x => x.Value <= lowerPercentile).Max(x=>x.Key);
            Double upperValue = lookup.Where(x => x.Value >= upperPercentile).Min(x => x.Key);
            return upperValue - lowerValue;
        }

        /// <summary>
        /// Prints a dictionary of double, double to Console.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static void ToConsole(this IDictionary<Double, Double> dictionary, String prepend = "", String append = "", Boolean sort = false)
        {
            IList<Double> keys;
            if (sort)
            {
                keys = dictionary.SortOnKeys().Keys.ToList();
            }
            else
            {
                keys = dictionary.Keys.ToList();
            }
            Console.Write(prepend);
            foreach (Double i in keys)
            {
                Console.WriteLine(String.Format("{0:0.0000} {1:0.0000}", i, dictionary[i]));
            }
            Console.Write(append);
        }

        /// <summary>
        /// Sorts a dictionary on its double key values
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="dictionary"></param>
        /// <returns>A SortedDictionary</returns>
        public static SortedDictionary<Double, T> SortOnKeys<T>(this IDictionary<Double, T> dictionary)
        {
            return new SortedDictionary<Double, T>(dictionary);
        }
    }
}
