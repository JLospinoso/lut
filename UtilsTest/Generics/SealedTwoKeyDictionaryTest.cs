﻿/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using NUnit.Framework;
using Lospi.Utils.Generics;
using System.Linq;
using System;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class SealedTwoKeyDictionaryTest
    {
        SealedTwoKeyDictionary<string, int, double> _dictionary;

        [SetUp]
        public void TwoKeyDictionaryTestSetUp()
        {
            string[] firstKeyStrings = { "a", "b" };
            int[] secondKeyInts = { 1, 2, 3 };

            _dictionary = new SealedTwoKeyDictionary<string, int, double>(firstKeyStrings, secondKeyInts);

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
        public void SealedTwoKeyDictionaryGettersEqualSetters()
        {
            Assert.That(_dictionary["b", 2], Is.EqualTo(5D));
        }

        [Test]
        public void SealedTwoKeyDictionaryKeyOneMarginalizerWorks()
        {
            var result = _dictionary.MarginalizeKeyOne("b");
            Assert.That(result[1], Is.EqualTo(4D));
            Assert.That(result[2], Is.EqualTo(5D));
            Assert.That(result[3], Is.EqualTo(6D));
        }

        [Test]
        public void SealedTwoKeyDictionaryKeyTwoMarginalizerWorks()
        {
            var result = _dictionary.MarginalizeKeyTwo(3);
            Assert.That(result["a"], Is.EqualTo(3D));
            Assert.That(result["b"], Is.EqualTo(6D));
        }


        [Test]
        public void SealedTwoKeyDictionaryTryGetValueReturnsValueIfPresent()
        {
            double result;

            _dictionary.TryGetValue("b", 2, out result);

            Assert.That(result, Is.EqualTo(5D));
        }

        [Test]
        public void SealedTwoKeyDictionaryTryGetValueReturnsValueIfNotPresent()
        {
            double result;

            _dictionary.TryGetValue("z", 2, out result);

            Assert.That(result, Is.EqualTo(0D));
        }

        [Test]
        public void SealedTwoKeyDictionaryKeysGivesAllKeyPairings()
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
        public void SealedTwoKeyDictionaryValuesGivesAllValues()
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
