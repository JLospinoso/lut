/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using NUnit.Framework;
using Lospi.Utils.Generics;
using System.Linq;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class SymmetricTwoKeyDictionaryTest
    {
        SymmetricTwoKeyDictionary<string, int> _dictionary;

        [SetUp]
        public void TwoKeyDictionaryTestSetUp()
        {
            string[] keys = { "a", "b", "c"};

            _dictionary = new SymmetricTwoKeyDictionary<string, int>(keys);

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
        public void SymmetricTwoKeyDictionaryGettersEqualSetters()
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
        public void SymmetricTwoKeyDictionaryMarginalizerWorks()
        {
            var result = _dictionary["a"];
            Assert.That(result["a"], Is.EqualTo(1));
            Assert.That(result["b"], Is.EqualTo(2));
            Assert.That(result["c"], Is.EqualTo(3));
        }

        [Test]
        public void SymmetricTwoKeyDictionaryAddIndexWorks()
        {
            _dictionary["d","a"] = 3;
            Assert.That(_dictionary["a", "d"], Is.EqualTo(3));
            Assert.That(_dictionary["b", "d"], Is.EqualTo(0));
            Assert.That(_dictionary["c", "d"], Is.EqualTo(0));
            Assert.That(_dictionary["d", "d"], Is.EqualTo(0));
        }

        [Test]
        public void SymmetricTwoKeyDictionaryAddTwoIndicesWorks()
        {
            _dictionary["d", "e"] = 5;
            Assert.That(_dictionary["d", "a"], Is.EqualTo(0));
            Assert.That(_dictionary["d", "b"], Is.EqualTo(0));
            Assert.That(_dictionary["d", "c"], Is.EqualTo(0));
            Assert.That(_dictionary["d", "d"], Is.EqualTo(0));
            Assert.That(_dictionary["d", "e"], Is.EqualTo(5));
            Assert.That(_dictionary["e", "a"], Is.EqualTo(0));
            Assert.That(_dictionary["e", "b"], Is.EqualTo(0));
            Assert.That(_dictionary["e", "c"], Is.EqualTo(0));
            Assert.That(_dictionary["e", "d"], Is.EqualTo(5));
            Assert.That(_dictionary["e", "e"], Is.EqualTo(0));
        }


        [Test]
        public void SymmetricTwoKeyDictionaryTryGetValueReturnsValue()
        {
            int result;

            _dictionary.TryGetValue("a", "c", out result);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void SymmetricTwoKeyDictionaryTryGetValueForInvalidKeyReturnsDefaultValue()
        {
            int result;

            _dictionary.TryGetValue("z", "z", out result);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SymmetricTwoKeyDictionaryKeysGivesAllKeyPairings()
        {
            var keys = _dictionary.Keys.ToList();

            Assert.That(keys, Contains.Item("a"));
            Assert.That(keys, Contains.Item("b"));
            Assert.That(keys, Contains.Item("c"));

        }

        [Test]
        public void SymmetricTwoKeyDictionaryValuesGivesAllValues()
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
