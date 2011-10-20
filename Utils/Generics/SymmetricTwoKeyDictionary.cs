/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A dictionary with two unordered (symmetric) keys. This dictionary uses each key's hashcode
    /// extensively, so ensure that the GetHashCode() methods are appropriately implemented.
    /// </summary>
    /// <typeparam name="Tk"></typeparam>
    /// <typeparam name="Tv"></typeparam>
    public class SymmetricTwoKeyDictionary<Tk, Tv> : ISymmetricTwoKeyDictionary<Tk, Tv>
    {
        /// <summary>
        /// Internal storage of the values
        /// </summary>
        Dictionary<Tk, Dictionary<Tk, Tv>> _internal;

        Dictionary<Tk, int> _hashTable;


        /// <summary>
        /// Default constructor
        /// </summary>
        public SymmetricTwoKeyDictionary()
        {
            _hashTable = new Dictionary<Tk, int>();

            CheckHashCodes();

            _internal = new Dictionary<Tk, Dictionary<Tk, Tv>>();

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstKeys">All of the possible first keys for this dictionary</param>
        /// <param name="secondKeys">All of the possible second keys for this dictionary</param>
        public SymmetricTwoKeyDictionary(IEnumerable<Tk> keys)
        {
            _hashTable = new Dictionary<Tk, int>();

            CheckHashCodes();

            _internal = new Dictionary<Tk, Dictionary<Tk, Tv>>();

            foreach (var key in keys)
            {
                _hashTable[key] = key.GetHashCode();
            }
            InitializeIndex(keys);
        }

        void InitializeIndex(Tk index)
        {
            _internal[index] = new Dictionary<Tk, Tv>();
            
            foreach (var lesserKey in GetLesserKeysInclusive(index))
            {
                _internal[index].Add(lesserKey, default(Tv));
            }

            foreach (var greaterKey in GetGreaterKeysExclusive(index))
            {
                _internal[greaterKey].Add(index, default(Tv));
            }
        }

        void InitializeIndex(IEnumerable<Tk> indices)
        {
            foreach (var index in indices)
            {
                _internal[index] = new Dictionary<Tk, Tv>();

                foreach (var lesserKey in GetLesserKeysInclusive(index))
                {
                    _internal[index].Add(lesserKey, default(Tv));
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

        void CheckIndicesAndExpand(Tk key1, Tk key2)
        {
            if (!_hashTable.ContainsKey(key1))
            {
                _hashTable[key1] = key1.GetHashCode();
                _internal[key1] = new Dictionary<Tk, Tv>();
                InitializeIndex(key1);
            }
            if (!_hashTable.ContainsKey(key2))
            {
                _hashTable[key2] = key2.GetHashCode();
                _internal[key2] = new Dictionary<Tk, Tv>();
                InitializeIndex(key2);
            }
        }

        IEnumerable<Tk> GetLesserKeysInclusive(Tk index)
        {
            return _hashTable.Keys.Where(x => _hashTable[x] <= _hashTable[index]);
        }

        IEnumerable<Tk> GetGreaterKeysExclusive(Tk index)
        {
            return _hashTable.Keys.Where(x => _hashTable[x] > _hashTable[index]);
        }
        
        void OrderKeys(ref Tk key1, ref Tk key2)
        {
            if (_hashTable[key1] < _hashTable[key2])
            {
                Tk temporary = key1;
                key1 = key2;
                key2 = temporary;
            }
        }

        bool OrderKeysSafe(ref Tk key1, ref Tk key2)
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
                Tk temporary = key1;
                key1 = key2;
                key2 = temporary;
            }

            return true;
        }

        /// <summary>
        /// A convenient getter and setter
        /// </summary>
        /// <param name="index1">Key one</param>
        /// <param name="index2">Key two</param>
        /// <returns>The corresponding value</returns>
        public Tv this[Tk key1, Tk key2]
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
        /// A convenient getter and setter
        /// </summary>
        /// <param name="index1">Key one</param>
        /// <param name="index2">Key two</param>
        /// <returns>The corresponding value</returns>
        public IDictionary<Tk,Tv> this[Tk key]
        {
            get { return Marginalize(key); }
        }

        public IDictionary<Tk,Tv> Marginalize(Tk index)
        {
            var result = new Dictionary<Tk, Tv>();

            foreach (Tk key in _hashTable.Keys)
            {
                result[key] = this[index, key];
            }

            return result;
        }

        public bool TryGetValue(Tk key1, Tk key2, out Tv value)
        {
            if (OrderKeysSafe(ref key1, ref key2))
            {
                value = this[key1, key2];
                return true;
            }
            else
            {
                value = default(Tv);
                return false;
            }
        }

        public IEnumerable<Tk> Keys
        {
            get
            {
                return _hashTable.Keys;
            }
        }

        public IEnumerable<Tv> Values
        {
            get
            {
                foreach (var key1 in _internal.Keys)
                {
                    foreach (var value in _internal[key1].Values)
                    {
                        yield return value;
                    }
                }
            }
        }
    }
}
