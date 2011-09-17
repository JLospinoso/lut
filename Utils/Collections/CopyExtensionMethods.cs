using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;

namespace Lospi.Utils.Collections
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Aids in deep copying collections of objects
        /// </summary>
        /// <typeparam name="To">A deep copyable type</typeparam>
        /// <param name="collection">A collection</param>
        /// <returns>A deep copy of the collection</returns>
        public static ICollection<T> DeepMemberwiseCopy<T>(this ICollection<T> collection)
            where T : IDeepCopyable<T>
        {
            HashSet<T> newList = new HashSet<T>();
            foreach (T item in collection)
            {
                newList.Add(item.DeepCopy());
            }
            return newList;
        }
    }
}
