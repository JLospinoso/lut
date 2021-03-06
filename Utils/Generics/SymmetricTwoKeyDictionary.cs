﻿/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A dictionary with two unordered (symmetric) keys. This dictionary uses each key's hashcode
    /// extensively, so ensure that the GetHashCode() methods are appropriately implemented.
    /// </summary>
    /// <typeparam name="TK"></typeparam>
    /// <typeparam name="TV"></typeparam>
    [Serializable]
    public class SymmetricTwoKeyDictionary<TK, TV> : ISymmetricTwoKeyDictionary<TK, TV>
    {
        /// <summary>
        /// Internal storage of the values
        /// </summary>
        readonly Dictionary<TK, Dictionary<TK, TV>> _internal;

        readonly Dictionary<TK, int> _hashTable;


        /// <summary>
        /// Default constructor
        /// </summary>
        public SymmetricTwoKeyDictionary()
        {
            _hashTable = new Dictionary<TK, int>();

            CheckHashCodes();

            _internal = new Dictionary<TK, Dictionary<TK, TV>>();

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keys">All of the possible keys for this dictionary</param>
        public SymmetricTwoKeyDictionary(ICollection<TK> keys)
        {
            _hashTable = new Dictionary<TK, int>();

            CheckHashCodes();

            _internal = new Dictionary<TK, Dictionary<TK, TV>>();

            foreach (var key in keys)
            {
                _hashTable[key] = key.GetHashCode();
            }
            InitializeIndex(keys);
        }

        void InitializeIndex(TK index)
        {
            _internal[index] = new Dictionary<TK, TV>();
            
            foreach (var lesserKey in GetLesserKeysInclusive(index))
            {
                _internal[index].Add(lesserKey, default(TV));
            }

            foreach (var greaterKey in GetGreaterKeysExclusive(index))
            {
                _internal[greaterKey].Add(index, default(TV));
            }
        }

        void InitializeIndex(IEnumerable<TK> indices)
        {
            foreach (var index in indices)
            {
                _internal[index] = new Dictionary<TK, TV>();

                foreach (var lesserKey in GetLesserKeysInclusive(index))
                {
                    _internal[index].Add(lesserKey, default(TV));
                }
            }
        }

        void CheckHashCodes()
        {
            if (_hashTable.Count != _hashTable.Values.Distinct().ToList().Count)
            {
                throw new ArgumentException(String.Format("Keys only contain {0} unique hashes but {1} elements.",
                    _hashTable.Count, _hashTable.Values.Distinct().ToList().Count));
            }
        }

        void CheckIndicesAndExpand(TK key1, TK key2)
        {
            if (!_hashTable.ContainsKey(key1))
            {
                _hashTable[key1] = key1.GetHashCode();
                _internal[key1] = new Dictionary<TK, TV>();
                InitializeIndex(key1);
            }
            if (!_hashTable.ContainsKey(key2))
            {
                _hashTable[key2] = key2.GetHashCode();
                _internal[key2] = new Dictionary<TK, TV>();
                InitializeIndex(key2);
            }
        }

        IEnumerable<TK> GetLesserKeysInclusive(TK index)
        {
            return _hashTable.Keys.Where(x => _hashTable[x] <= _hashTable[index]);
        }

        IEnumerable<TK> GetGreaterKeysExclusive(TK index)
        {
            return _hashTable.Keys.Where(x => _hashTable[x] > _hashTable[index]);
        }
        
        void OrderKeys(ref TK key1, ref TK key2)
        {
            if (_hashTable[key1] < _hashTable[key2])
            {
                TK temporary = key1;
                key1 = key2;
                key2 = temporary;
            }
        }

        bool OrderKeysSafe(ref TK key1, ref TK key2)
        {
            int hashOne, hashTwo;

            var hashOnePresent = _hashTable.TryGetValue(key1, out hashOne);
            var hashTwoPresent = _hashTable.TryGetValue(key2, out hashTwo);

            if (!hashOnePresent || !hashTwoPresent)
            {
                return false;
            }

            if (hashOne < hashTwo)
            {
                TK temporary = key1;
                key1 = key2;
                key2 = temporary;
            }

            return true;
        }

        /// <summary>
        /// A convenient getter and setter
        /// </summary>
        /// <param name="key1">Key one</param>
        /// <param name="key2">Key two</param>
        /// <returns>The corresponding value</returns>
        public TV this[TK key1, TK key2]
        {
            get
            {
                OrderKeys(ref key1, ref key2);
                return _internal[key1][key2];
            }
            set
            {
                CheckIndicesAndExpand(key1, key2);
                OrderKeys(ref key1, ref key2);
                _internal[key1][key2] = value;
            }
        }

        /// <summary>
        /// A convenient marginalizer 
        /// </summary>
        /// <param name="key">Key one</param>
        /// <returns>The corresponding value</returns>
        public IDictionary<TK,TV> this[TK key]
        {
            get { return Marginalize(key); }
        }

        public IDictionary<TK,TV> Marginalize(TK index)
        {
            var result = new Dictionary<TK, TV>();

            foreach (TK key in _hashTable.Keys)
            {
                result[key] = this[index, key];
            }

            return result;
        }

        public bool TryGetValue(TK key1, TK key2, out TV value)
        {
            if (OrderKeysSafe(ref key1, ref key2))
            {
                value = this[key1, key2];
                return true;
            }
            value = default(TV);
            return false;
        }

        public IEnumerable<TK> Keys
        {
            get
            {
                return _hashTable.Keys;
            }
        }

        public IEnumerable<TV> Values
        {
            get { return _internal.Keys.SelectMany(key1 => _internal[key1].Values);  }
        }
    }
}
