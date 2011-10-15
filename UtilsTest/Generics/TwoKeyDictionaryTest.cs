/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using NUnit.Framework;
using Lospi.Utils.Generics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class TwoKeyDictionaryTest
    {
        TwoKeyDictionary<string, int, double> dict;

        [SetUp]
        public void TwoKeyDictionaryTestSetUp()
        {
            string[] firstKeyStrings = { "a", "b" };
            int[] secondKeyInts = { 1, 2, 3 };

            dict = new TwoKeyDictionary<string, int, double>(firstKeyStrings, secondKeyInts);

            dict["a", 1] = 1D;
            dict["a", 2] = 2D;
            dict["a", 3] = 3D;

            dict["b", 1] = 4D;
            dict["b", 2] = 5D;
            dict["b", 3] = 6D;
        }

        [TearDown]
        public void TwoKeyDictionaryTestTearDown()
        {
            dict = null;
        }

        [Test]
        public void TwoKeyDictionary_Getters_EqualSetters()
        {
            Assert.That(dict["b", 2], Is.EqualTo(5D));
        }

        [Test]
        public void TwoKeyDictionary_KeyOneMarginalizer_Works()
        {
            var result = dict.MarginalizeKeyOne("b");
            Assert.That(result[1], Is.EqualTo(4D));
            Assert.That(result[2], Is.EqualTo(5D));
            Assert.That(result[3], Is.EqualTo(6D));
        }

        [Test]
        public void TwoKeyDictionary_KeyTwoMarginalizer_Works()
        {
            var result = dict.MarginalizeKeyTwo(3);
            Assert.That(result["a"], Is.EqualTo(3D));
            Assert.That(result["b"], Is.EqualTo(6D));
        }

        [Test]
        public void TwoKeyDictionary_AddSecondIndex_Works()
        {
            dict["b", 0] = 1D;
            Assert.That(dict["b", 0], Is.EqualTo(1D));
            Assert.That(dict["a", 0], Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionary_AddFirstIndex_Works()
        {
            dict["c", 1] = 2D;
            Assert.That(dict["c", 1], Is.EqualTo(2D));
            Assert.That(dict["c", 2], Is.EqualTo(0D));
            Assert.That(dict["c", 3], Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionary_AddFirstAndSecondIndex_Works()
        {
            dict["c", 0] = 2D;
            Assert.That(dict["a", 0], Is.EqualTo(0D));
            Assert.That(dict["b", 0], Is.EqualTo(0D));
            Assert.That(dict["c", 0], Is.EqualTo(2D));
            Assert.That(dict["c", 1], Is.EqualTo(0D));
            Assert.That(dict["c", 2], Is.EqualTo(0D));
            Assert.That(dict["c", 3], Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionary_TryGetValue_ReturnsValueIfPresent()
        {
            double result;

            dict.TryGetValue("b", 2, out result);

            Assert.That(result, Is.EqualTo(5D));
        }

        [Test]
        public void TwoKeyDictionary_TryGetValue_ReturnsDefaultIfNotPresent()
        {
            double result;

            dict.TryGetValue("z", 2, out result);

            Assert.That(result, Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionary_Keys_GivesAllKeyPairings()
        {
            var keys = dict.Keys.ToList();

            Assert.That(keys, Contains.Item(new Tuple<string, int>("a", 1)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("a", 2)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("a", 3)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("b", 1)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("b", 2)));
            Assert.That(keys, Contains.Item(new Tuple<string, int>("b", 3)));
        }

        [Test]
        public void TwoKeyDictionary_Values_GivesAllValues()
        {
            var values = dict.Values.ToList();

            Assert.That(values, Contains.Item(1D));
            Assert.That(values, Contains.Item(2D));
            Assert.That(values, Contains.Item(3D));
            Assert.That(values, Contains.Item(4D));
            Assert.That(values, Contains.Item(5D));
            Assert.That(values, Contains.Item(6D));
        }
    }
}
