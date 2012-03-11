/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A custom dictionary that accepts two keys. You can consider each key pair as a unique
    /// key1. This dictionary is useful for not having to create custom object pairs.
    /// You must ensure that, like a normal IDictionary, the keys produce unique GetHashcode().
    /// </summary>
    /// <typeparam name="TK1">The type of key 1</typeparam>
    /// <typeparam name="TK2">The type of key 2</typeparam>
    /// <typeparam name="TV">The type of the value</typeparam>
    [Serializable]
    public class TwoKeyDictionary<TK1, TK2, TV> : ITwoKeyDictionary<TK1, TK2, TV>
    {
        /// <summary>
        /// Internal storage of the values
        /// </summary>
        readonly Dictionary<TK1, Dictionary<TK2, TV>> _internal;

        /// <summary>
        /// A collection of all key 1 values
        /// </summary>
        public ICollection<TK1> FirstKey { get; protected set; }

        /// <summary>
        /// A collection of all key 2 values
        /// </summary>
        public ICollection<TK2> SecondKey { get; protected set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TwoKeyDictionary()
        {
            FirstKey = new List<TK1>();
            SecondKey = new List<TK2>();

            CheckHashCodes();

            _internal = new Dictionary<TK1, Dictionary<TK2, TV>>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstKeys">All of the possible first keys for this dictionary</param>
        /// <param name="secondKeys">All of the possible second keys for this dictionary</param>
        public TwoKeyDictionary(IEnumerable<TK1> firstKeys, IEnumerable<TK2> secondKeys)
        {
            FirstKey = firstKeys.ToList();
            SecondKey = secondKeys.ToList();

            CheckHashCodes();

            _internal = new Dictionary<TK1, Dictionary<TK2, TV>>();

            foreach (TK1 key1 in FirstKey)
            {
                _internal[key1] = new Dictionary<TK2, TV>();
                foreach (TK2 key2 in SecondKey)
                {
                    _internal[key1][key2] = default(TV);
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

        void CheckIndicesAndExpand(TK1 index1, TK2 index2)
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

        void AddKeyOneIndex(TK1 index)
        {
            _internal[index] = new Dictionary<TK2, TV>();
            foreach (TK2 key2 in SecondKey)
            {
                _internal[index][key2] = default(TV);
            }
            FirstKey.Add(index);
        }

        void AddKeyTwoIndex(TK2 index)
        {
            foreach (TK1 key1 in FirstKey)
            {
                _internal[key1][index] = default(TV);
            }
            SecondKey.Add(index);
        }

        /// <summary>
        /// A convenient getter and setter
        /// </summary>
        /// <param name="key1">Key one</param>
        /// <param name="key2">Key two</param>
        /// <returns>The corresponding value</returns>
        public TV this[TK1 key1, TK2 key2]
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
        /// A convenient getter and setter
        /// </summary>
        /// <param name="key">The key as a tuple</param>
        /// <returns>The corresponding value</returns>
        public TV this[ Tuple<TK1, TK2> key]
        {
            get
            {
                return _internal[key.Item1][key.Item2];
            }
            set
            {
                CheckIndicesAndExpand(key.Item1, key.Item2);
                _internal[key.Item1][key.Item2] = value;
            }
        }

        /// <summary>
        /// Returns a marginalized, single key dictionary over a key one
        /// key1
        /// </summary>
        /// <param name="index">Index over which to marginalize</param>
        /// <returns></returns>
        public IDictionary<TK2, TV> MarginalizeKeyOne(TK1 index)
        {
            return _internal[index];
        }

        /// <summary>
        /// Returns a marginalized, single key dictionary over a key two
        /// key1
        /// </summary>
        /// <param name="index">Index over which to marginalize</param>
        /// <returns></returns>
        public IDictionary<TK1, TV> MarginalizeKeyTwo(TK2 index)
        {
            var result = new Dictionary<TK1, TV>();
            foreach (TK1 key1 in _internal.Keys)
            {
                result[key1] = _internal[key1][index];
            }
            return result;
        }

        public bool TryGetValue(TK1 key1, TK2 key2, out TV value)
        {
            Dictionary<TK2, TV> intermediate;

            _internal.TryGetValue(key1, out intermediate);

            if (intermediate == null)
            {
                value = default(TV);
                return false;
            }

            return intermediate.TryGetValue(key2, out value);
        }

        public IEnumerable<Tuple<TK1, TK2>> Keys
        {
            get { return from key1 in _internal.Keys from key2 in _internal[key1].Keys select new Tuple<TK1, TK2>(key1, key2);  }
        }

        public IEnumerable<TV> Values
        {
            get { return from key1 in _internal.Keys from key2 in _internal[key1].Keys select this[key1, key2]; }
        }
    }
}
