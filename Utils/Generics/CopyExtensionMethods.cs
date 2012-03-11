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

        /// <summary>
        /// Copies a dictionary with DeepCopyable keys and value type values into another Dictionary
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static Dictionary<TK, TV> DeepCopyToDictionary<TK, TV>(this IDictionary<TK, TV> dictionary)
            where TK : IDeepCopyable<TK>
            where TV : struct
        {
            return dictionary.ToDictionary(keyValue => keyValue.Key.DeepCopy(), keyValue => keyValue.Value);
        }

        public static TwoKeyDictionary<TK1, TK2, TV> DeepCopyToTwoKeyDictionary<TK1, TK2, TV>(this TwoKeyDictionary<TK1, TK2, TV> dictionary)
            where TK1 : IDeepCopyable<TK1>
            where TK2 : IDeepCopyable<TK2>
            where TV : struct 
        {
            var copy = new TwoKeyDictionary<TK1, TK2, TV>(dictionary.FirstKey, dictionary.SecondKey);
            foreach(var key in dictionary.Keys)
            {
                var key1Copy = key.Item1.DeepCopy();
                var key2Copy = key.Item2.DeepCopy();
                copy[key1Copy, key2Copy] = dictionary[key];
            }
            return copy;
        }
    }
}
