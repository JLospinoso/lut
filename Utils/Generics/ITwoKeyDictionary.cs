/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System;
using System.Collections.Generic;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A custom dictionary that accepts two keys. You can consider each key pair as a unique
    /// key1. This dictionary is useful for not having to create custom object pairs.
    /// </summary>
    /// <typeparam name="TK1">The type of key 1</typeparam>
    /// <typeparam name="TK2">The type of key 2</typeparam>
    /// <typeparam name="TV">The type of the value</typeparam>
    public interface ITwoKeyDictionary<TK1, TK2, TV>
    {

        /// <summary>
        /// Bracket operator as getter and setter
        /// </summary>
        /// <param name="key1">Key one</param>
        /// <param name="key2">Key two</param>
        /// <returns>The corresponding value</returns>
        TV this[TK1 key1, TK2 key2] { get; set; }

        /// <summary>
        /// Returns a marginalized, single key dictionary over a
        /// key1
        /// </summary>
        /// <param name="index">Index over which to marginalize</param>
        /// <returns></returns>
        IDictionary<TK2, TV> MarginalizeKeyOne(TK1 index);

        /// <summary>
        /// Returns a marginalized, single key dictionary over a value
        /// </summary>
        /// <param name="index">Index over which to marginalize</param>
        /// <returns></returns>
        IDictionary<TK1, TV> MarginalizeKeyTwo(TK2 index);

        /// <summary>
        /// Gets the value associated with the specified keys.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(TK1 key1, TK2 key2, out TV value);

        /// <summary>
        /// All key pairings contained in the dictionary
        /// </summary>
        IEnumerable<Tuple<TK1, TK2>> Keys { get; }

        /// <summary>
        /// All values contained in the dictionary
        /// </summary>
        IEnumerable<TV> Values { get; }
    }
}
