/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */
using System.Collections.Generic;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A dictionary with two unordered (symmetric) keys.
    /// </summary>
    /// <typeparam name="TK">The type of the key</typeparam>
    /// <typeparam name="TV">The type of the value</typeparam>
    public interface ISymmetricTwoKeyDictionary<TK, TV>
    {

        /// <summary>
        /// Bracket operator as getter and setter
        /// </summary>
        /// <param name="key1">Key one</param>
        /// <param name="key2">Key two</param>
        /// <returns>The corresponding value</returns>
        TV this[TK key1, TK key2] { get; set; }

        /// <summary>
        /// Returns a marginalized, single key dictionary
        /// </summary>
        /// <param name="index">Index over which to marginalize</param>
        /// <returns></returns>
        IDictionary<TK, TV> Marginalize(TK index);
        
        /// <summary>
        /// Bracket operator for marginalization
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IDictionary<TK, TV> this[TK key] { get; }

        /// <summary>
        /// Gets the value associated with the specified keys.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(TK key1, TK key2, out TV value);

        /// <summary>
        /// All key pairings contained in the dictionary
        /// </summary>
        IEnumerable<TK> Keys { get; }

        /// <summary>
        /// All values contained in the dictionary
        /// </summary>
        IEnumerable<TV> Values { get; }
    }
}
