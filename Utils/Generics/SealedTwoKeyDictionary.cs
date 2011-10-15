using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lospi.Utils.Generics
{
    /// <summary>
    /// A custom dictionary that accepts two keys. You can consider each key pair as a unique
    /// key1. This dictionary is useful for not having to create custom object pairs.
    /// You must ensure that, like a normal IDictionary, the keys produce unique GetHashcode().
    /// You cannot add or remove keys once the object is initialized.
    /// </summary>
    /// <typeparam name="Tk1">The type of key 1</typeparam>
    /// <typeparam name="Tk2">The type of key 2</typeparam>
    /// <typeparam name="Tv">The type of the value</typeparam>
    public class SealedTwoKeyDictionary<Tk1, Tk2, Tv> : ITwoKeyDictionary<Tk1, Tk2, Tv>
    {
        /// <summary>
        /// Internal storage of the values
        /// </summary>
        Dictionary<Tk1, Dictionary<Tk2, Tv>> _internal;

        /// <summary>
        /// A deepCopyable of all key 1 values
        /// </summary>
        protected ICollection<Tk1> FirstKey { get; set; }

        /// <summary>
        /// A deepCopyable of all key 2 values
        /// </summary>
        protected ICollection<Tk2> SecondKey { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="firstKeys">All of the possible first keys for this dictionary</param>
        /// <param name="secondKeys">All of the possible second keys for this dictionary</param>
        public SealedTwoKeyDictionary(IEnumerable<Tk1> firstKeys, IEnumerable<Tk2> secondKeys)
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
                _internal[key1][key2] = value;
            }
        }

        /// <summary>
        /// Returns a marginalized, single key dictionary over a key one
        /// key1
        /// </summary>
        /// <param name="key1">Index over which to marginalize</param>
        /// <returns></returns>
        public IDictionary<Tk2, Tv> MarginalizeKeyOne(Tk1 index)
        {
            return _internal[index];
        }

        /// <summary>
        /// Returns a marginalized, single key dictionary over a key two
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

        public bool TryGetValue(Tk1 key1, Tk2 key2, out Tv value)
        {
            Dictionary<Tk2, Tv> intermediate;

            _internal.TryGetValue(key1, out intermediate);

            if (intermediate == null)
            {
                value = default(Tv);
                return false;
            }

            return intermediate.TryGetValue(key2, out value);
        }

        public IEnumerable<Tuple<Tk1, Tk2>> Keys
        {
            get
            {
                foreach (var key1 in _internal.Keys)
                {
                    foreach (var key2 in _internal[key1].Keys)
                    {
                        yield return new Tuple<Tk1, Tk2>(key1, key2);
                    }
                }
            }
        }

        public IEnumerable<Tv> Values
        {
            get
            {
                foreach (var key1 in _internal.Keys)
                {
                    foreach (var key2 in _internal[key1].Keys)
                    {
                        yield return this[key1, key2];
                    }
                }
            }
        }
    }
}
