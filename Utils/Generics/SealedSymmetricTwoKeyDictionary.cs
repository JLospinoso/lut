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
    /// This dictionary is sealed so that you cannot add or remove keys.
    /// </summary>
    /// <typeparam name="Tk"></typeparam>
    /// <typeparam name="Tv"></typeparam>
    public class SealedSymmetricTwoKeyDictionary<Tk, Tv>
    {
        /// <summary>
        /// Internal storage of the values
        /// </summary>
        Dictionary<Tk, Dictionary<Tk, Tv>> _internal;

        Dictionary<Tk, int> _hashTable;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="firstKeys">All of the possible first keys for this dictionary</param>
        /// <param name="secondKeys">All of the possible second keys for this dictionary</param>
        public SealedSymmetricTwoKeyDictionary(IEnumerable<Tk> keys)
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

        IDictionary<Tk,Tv> Marginalize(Tk index)
        {
            var result = new Dictionary<Tk, Tv>();

            foreach (Tk key in _hashTable.Keys)
            {
                result[key] = this[index, key];
            }

            return result;
        }
    }
}
