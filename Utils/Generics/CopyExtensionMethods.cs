/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;

namespace Lospi.Utils.Generics
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Aids in deep copying collections of objects
        /// </summary>
        /// <typeparam name="To">A deep copyable type</typeparam>
        /// <param name="deepCopyable">A deepCopyable</param>
        /// <returns>A deep copy of the deepCopyable</returns>
        public static IEnumerable<T> DeepMemberwiseCopy<T>(this IEnumerable<T> deepCopyable)
            where T : IDeepCopyable<T>
        {
            foreach (T item in deepCopyable)
            {
                yield return item.DeepCopy();
            }
        }
    }
}
