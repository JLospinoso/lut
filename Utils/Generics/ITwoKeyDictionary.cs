using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A custom dictionary that accepts two keys. You can consider each key pair as a unique
    /// key1. This dictionary is useful for not having to create custom object pairs.
    /// </summary>
    /// <typeparam name="Tk1">The type of key 1</typeparam>
    /// <typeparam name="Tk2">The type of key 2</typeparam>
    /// <typeparam name="Tv">The type of the value</typeparam>
    public interface ITwoKeyDictionary<Tk1, Tk2, Tv>
    {

        /// <summary>
        /// Bracket operator as getter and setter
        /// </summary>
        /// <param name="index1">Key one</param>
        /// <param name="index2">Key two</param>
        /// <returns>The corresponding value</returns>
        Tv this[Tk1 key1, Tk2 key2] { get; set; }

        /// <summary>
        /// Returns a marginalized, single key dictionary over a
        /// key1
        /// </summary>
        /// <param name="key1">Index over which to marginalize</param>
        /// <returns></returns>
        IDictionary<Tk2, Tv> MarginalizeKeyOne(Tk1 index);

        /// <summary>
        /// Returns a marginalized, single key dictionary over a value
        /// </summary>
        /// <param name="key1">Index over which to marginalize</param>
        /// <returns></returns>
        IDictionary<Tk1, Tv> MarginalizeKeyTwo(Tk2 index);

        /// <summary>
        /// Gets the value associated with the specified keys.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="value"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(Tk1 key1, Tk2 key2, out Tv value);

        /// <summary>
        /// All key pairings contained in the dictionary
        /// </summary>
        IEnumerable<Tuple<Tk1, Tk2>> Keys { get; }

        /// <summary>
        /// All values contained in the dictionary
        /// </summary>
        IEnumerable<Tv> Values { get; }
    }
}
