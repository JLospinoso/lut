/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System.Linq;
using Lospi.Utils.Generics;
using NUnit.Framework;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class SealedSymmetricTwoKeyDictionaryTest
    {
        SealedSymmetricTwoKeyDictionary<string, int> _dictionary;

        [SetUp]
        public void TwoKeyDictionaryTestSetUp()
        {
            string[] keys = { "a", "b", "c"};

            _dictionary = new SealedSymmetricTwoKeyDictionary<string, int>(keys);

            _dictionary["a", "a"] = 1;
            _dictionary["a", "b"] = 2;
            _dictionary["a", "c"] = 3;
            _dictionary["b", "b"] = 4;
            _dictionary["b", "c"] = 5;
            _dictionary["c", "c"] = 6;
        }

        [TearDown]
        public void TwoKeyDictionaryTestTearDown()
        {
            _dictionary = null;
        }

        [Test]
        public void SealedSymmetricTwoKeyDictionaryGettersEqualSetters()
        {
            Assert.That(_dictionary["a", "a"], Is.EqualTo(1));
            Assert.That(_dictionary["b", "a"], Is.EqualTo(2));
            Assert.That(_dictionary["a", "b"], Is.EqualTo(2));
            Assert.That(_dictionary["a", "c"], Is.EqualTo(3));
            Assert.That(_dictionary["c", "a"], Is.EqualTo(3));
            Assert.That(_dictionary["b", "b"], Is.EqualTo(4));
            Assert.That(_dictionary["b", "c"], Is.EqualTo(5));
            Assert.That(_dictionary["c", "b"], Is.EqualTo(5));
            Assert.That(_dictionary["c", "c"], Is.EqualTo(6));
        }

        [Test]
        public void SealedSymmetricTwoKeyDictionaryMarginalizerWorks()
        {
            var result = _dictionary["a"];
            Assert.That(result["a"], Is.EqualTo(1));
            Assert.That(result["b"], Is.EqualTo(2));
            Assert.That(result["c"], Is.EqualTo(3));
        }

        [Test]
        public void SealedSymmetricTwoKeyDictionaryTryGetValueReturnsValue()
        {
            int result;

            _dictionary.TryGetValue("a", "c", out result);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void SealedSymmetricTwoKeyDictionaryTryGetValueForInvalidKeyReturnsDefaultValue()
        {
            int result;

            _dictionary.TryGetValue("z", "z", out result);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SealedSymmetricTwoKeyDictionaryKeysGivesAllKeyPairings()
        {
            var keys = _dictionary.Keys.ToList();

            Assert.That(keys, Contains.Item("a"));
            Assert.That(keys, Contains.Item("b"));
            Assert.That(keys, Contains.Item("c"));

        }

        [Test]
        public void SealedSymmetricTwoKeyDictionaryValuesGivesAllValues()
        {
            var values = _dictionary.Values.ToList();

            Assert.That(values, Contains.Item(1));
            Assert.That(values, Contains.Item(2));
            Assert.That(values, Contains.Item(3));
            Assert.That(values, Contains.Item(4));
            Assert.That(values, Contains.Item(5));
            Assert.That(values, Contains.Item(6));
        }
    }
}
