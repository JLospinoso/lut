/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using NUnit.Framework;
using Lospi.Utils.Generics;
using System.Linq;
using System;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class TwoKeyDictionaryTest
    {
        TwoKeyDictionary<string, int, double> _dictionary;

        [SetUp]
        public void TwoKeyDictionaryTestSetUp()
        {
            string[] firstKeyStrings = { "a", "b" };
            int[] secondKeyInts = { 1, 2, 3 };

            _dictionary = new TwoKeyDictionary<string, int, double>(firstKeyStrings, secondKeyInts);

            _dictionary["a", 1] = 1D;
            _dictionary["a", 2] = 2D;
            _dictionary["a", 3] = 3D;

            _dictionary["b", 1] = 4D;
            _dictionary["b", 2] = 5D;
            _dictionary["b", 3] = 6D;
        }

        [TearDown]
        public void TwoKeyDictionaryTestTearDown()
        {
            _dictionary = null;
        }

        [Test]
        public void TwoKeyDictionaryGettersEqualSetters()
        {
            Assert.That(_dictionary["b", 2], Is.EqualTo(5D));
        }

        [Test]
        public void TwoKeyDictionaryKeyOneMarginalizerWorks()
        {
            var result = _dictionary.MarginalizeKeyOne("b");
            Assert.That(result[1], Is.EqualTo(4D));
            Assert.That(result[2], Is.EqualTo(5D));
            Assert.That(result[3], Is.EqualTo(6D));
        }

        [Test]
        public void TwoKeyDictionaryKeyTwoMarginalizerWorks()
        {
            var result = _dictionary.MarginalizeKeyTwo(3);
            Assert.That(result["a"], Is.EqualTo(3D));
            Assert.That(result["b"], Is.EqualTo(6D));
        }

        [Test]
        public void TwoKeyDictionaryAddSecondIndexWorks()
        {
            _dictionary["b", 0] = 1D;
            Assert.That(_dictionary["b", 0], Is.EqualTo(1D));
            Assert.That(_dictionary["a", 0], Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionaryAddFirstIndexWorks()
        {
            _dictionary["c", 1] = 2D;
            Assert.That(_dictionary["c", 1], Is.EqualTo(2D));
            Assert.That(_dictionary["c", 2], Is.EqualTo(0D));
            Assert.That(_dictionary["c", 3], Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionaryAddFirstAndSecondIndexWorks()
        {
            _dictionary["c", 0] = 2D;
            Assert.That(_dictionary["a", 0], Is.EqualTo(0D));
            Assert.That(_dictionary["b", 0], Is.EqualTo(0D));
            Assert.That(_dictionary["c", 0], Is.EqualTo(2D));
            Assert.That(_dictionary["c", 1], Is.EqualTo(0D));
            Assert.That(_dictionary["c", 2], Is.EqualTo(0D));
            Assert.That(_dictionary["c", 3], Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionaryTryGetValueReturnsValueIfPresent()
        {
            double result;

            _dictionary.TryGetValue("b", 2, out result);

            Assert.That(result, Is.EqualTo(5D));
        }

        [Test]
        public void TwoKeyDictionaryTryGetValueReturnsDefaultIfNotPresent()
        {
            double result;

            _dictionary.TryGetValue("z", 2, out result);

            Assert.That(result, Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionaryKeysGivesAllKeyPairings()
        {
            var keys = _dictionary.Keys.ToList();

            Assert.That(keys, Contains.Item(new Tuple<string, int>("a", 1)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("a", 2)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("a", 3)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("b", 1)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("b", 2)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("b", 3)));
        }

        [Test]
        public void TwoKeyDictionaryValuesGivesAllValues()
        {
            var values = _dictionary.Values.ToList();

            Assert.That(values, Contains.Item(1D));
            Assert.That(values, Contains.Item(2D));
            Assert.That(values, Contains.Item(3D));
            Assert.That(values, Contains.Item(4D));
            Assert.That(values, Contains.Item(5D));
            Assert.That(values, Contains.Item(6D));
        }
    }
}
