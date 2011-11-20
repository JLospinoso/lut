/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System.Collections.Generic;
using System.Linq;

namespace Lospi.Utils.Generics
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Aids in deep copying collections of objects
        /// </summary>
        /// <typeparam name="T">A deep copyable type</typeparam>
        /// <param name="deepCopyable">A deepCopyable</param>
        /// <returns>A deep copy of the deepCopyable</returns>
        public static IEnumerable<T> DeepMemberwiseCopy<T>(this IEnumerable<T> deepCopyable)
            where T : IDeepCopyable<T>
        {
            return deepCopyable.Select(item => item.DeepCopy());
        }
    }
}
