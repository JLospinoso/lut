using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A custom dictionary that accepts two keys. You can consider each lesserKey pair as a unique
    /// key1. This dictionary is useful for not having to create custom object pairs.
    /// You must ensure that, like a normal IDictionary, the keys produce unique GetHashcode().
    /// </summary>
    /// <typeparam name="Tk1">The type of lesserKey 1</typeparam>
    /// <typeparam name="Tk2">The type of lesserKey 2</typeparam>
    /// <typeparam name="Tv">The type of the value</typeparam>
    public class TwoKeyDictionary<Tk1, Tk2, Tv>
    {
        /// <summary>
        /// Internal storage of the values
        /// </summary>
        Dictionary<Tk1, Dictionary<Tk2, Tv>> _internal;

        /// <summary>
        /// A deepCopyable of all lesserKey 1 values
        /// </summary>
        protected ICollection<Tk1> FirstKey { get; set; }

        /// <summary>
        /// A deepCopyable of all lesserKey 2 values
        /// </summary>
        protected ICollection<Tk2> SecondKey { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="firstKeys">All of the possible first keys for this dictionary</param>
        /// <param name="secondKeys">All of the possible second keys for this dictionary</param>
        public TwoKeyDictionary(IEnumerable<Tk1> firstKeys, IEnumerable<Tk2> secondKeys)
        {
            FirstKey = firstKeys.ToList();
            SecondKey = secondKeys.ToList();

            CheckHashCodes();

            _internal = new Dictionary<Tk1, Dictionary<Tk2, Tv>>();

            foreach (Tk1 key1 in FirstKey)
            {
                _internal[key1] = new Dictionary<Tk2, Tv>();
                foreach (Tk2 key2 in SecondKey)
                {
                    _internal[key1][key2] = default(Tv);
                }
            }
        }

        void CheckHashCodes()
        {
            int uniqueKeyOneHashes = FirstKey.Select(x => x.GetHashCode()).Distinct().Count();

            int uniqueKeyTwoHashes = SecondKey.Select(x => x.GetHashCode()).Distinct().Count();

            if (uniqueKeyOneHashes != FirstKey.Count)
            {
                throw new ArgumentException(String.Format("FirstKeys only contains {0} unique hashes but {1} elements.", uniqueKeyOneHashes, FirstKey.Count));
            }

            if (uniqueKeyTwoHashes != SecondKey.Count)
            {
                throw new ArgumentException(String.Format("SecondKeys only contains {0} unique hashes but {1} elements.", uniqueKeyTwoHashes, SecondKey.Count));
            }
        }

        void CheckIndicesAndExpand(Tk1 index1, Tk2 index2)
        {
            if (!FirstKey.Contains(index1))
            {
                AddKeyOneIndex(index1);
            }
            if (!SecondKey.Contains(index2))
            {
                AddKeyTwoIndex(index2);
            }
        }

        void AddKeyOneIndex(Tk1 index)
        {
            _internal[index] = new Dictionary<Tk2, Tv>();
            foreach (Tk2 key2 in SecondKey)
            {
                _internal[index][key2] = default(Tv);
            }
            FirstKey.Add(index);
        }

        void AddKeyTwoIndex(Tk2 index)
        {
            foreach (Tk1 key1 in FirstKey)
            {
                _internal[key1][index] = default(Tv);
            }
            SecondKey.Add(index);
        }

        /// <summary>
        /// A convenient getter and setter
        /// </summary>
        /// <param name="index1">Key one</param>
        /// <param name="index2">Key two</param>
        /// <returns>The corresponding value</returns>
        public Tv this[Tk1 key1, Tk2 key2]
        {
            get
            {
                return _internal[key1][key2];
            }
            set
            {
                CheckIndicesAndExpand(key1, key2);
                _internal[key1][key2] = value;
            }
        }

        /// <summary>
        /// Returns a marginalized, single lesserKey dictionary over a lesserKey one
        /// key1
        /// </summary>
        /// <param name="key1">Index over which to marginalize</param>
        /// <returns></returns>
        public IDictionary<Tk2, Tv> MarginalizeKeyOne(Tk1 index)
        {
            return _internal[index];
        }

        /// <summary>
        /// Returns a marginalized, single lesserKey dictionary over a lesserKey two
        /// key1
        /// </summary>
        /// <param name="key1">Index over which to marginalize</param>
        /// <returns></returns>
        public IDictionary<Tk1, Tv> MarginalizeKeyTwo(Tk2 index)
        {
            var result = new Dictionary<Tk1, Tv>();
            foreach (Tk1 key1 in _internal.Keys)
            {
                result[key1] = _internal[key1][index];
            }
            return result;
        }
    }
}
