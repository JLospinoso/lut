using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A dictionary with two unordered (symmetric) keys.
    /// </summary>
    /// <typeparam name="Tk">The type of the key</typeparam>
    /// <typeparam name="Tv">The type of the value</typeparam>
    public interface ISymmetricTwoKeyDictionary<Tk, Tv>
    {

        /// <summary>
        /// Bracket operator as getter and setter
        /// </summary>
        /// <param name="index1">Key one</param>
        /// <param name="index2">Key two</param>
        /// <returns>The corresponding value</returns>
        Tv this[Tk key1, Tk key2] { get; set; }

        /// <summary>
        /// Returns a marginalized, single key dictionary
        /// </summary>
        /// <param name="key1">Index over which to marginalize</param>
        /// <returns></returns>
        IDictionary<Tk, Tv> Marginalize(Tk index);
        
        /// <summary>
        /// Bracket operator for marginalization
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IDictionary<Tk, Tv> this[Tk key] { get; }

        /// <summary>
        /// Gets the value associated with the specified keys.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="value"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(Tk key1, Tk key2, out Tv value);

        /// <summary>
        /// All key pairings contained in the dictionary
        /// </summary>
        IEnumerable<Tk> Keys { get; }

        /// <summary>
        /// All values contained in the dictionary
        /// </summary>
        IEnumerable<Tv> Values { get; }
    }
}
