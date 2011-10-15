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
    public class SymmetricTwoKeyDictionaryTest
    {
        SymmetricTwoKeyDictionary<string, int> dict;

        [SetUp]
        public void TwoKeyDictionaryTestSetUp()
        {
            string[] keys = { "a", "b", "c"};

            dict = new SymmetricTwoKeyDictionary<string, int>(keys);

            dict["a", "a"] = 1;
            dict["a", "b"] = 2;
            dict["a", "c"] = 3;
            dict["b", "b"] = 4;
            dict["b", "c"] = 5;
            dict["c", "c"] = 6;
        }

        [TearDown]
        public void TwoKeyDictionaryTestTearDown()
        {
            dict = null;
        }

        [Test]
        public void SymmetricTwoKeyDictionary_Getters_EqualSetters()
        {
            Assert.That(dict["a", "a"], Is.EqualTo(1));
            Assert.That(dict["b", "a"], Is.EqualTo(2));
            Assert.That(dict["a", "b"], Is.EqualTo(2));
            Assert.That(dict["a", "c"], Is.EqualTo(3));
            Assert.That(dict["c", "a"], Is.EqualTo(3));
            Assert.That(dict["b", "b"], Is.EqualTo(4));
            Assert.That(dict["b", "c"], Is.EqualTo(5));
            Assert.That(dict["c", "b"], Is.EqualTo(5));
            Assert.That(dict["c", "c"], Is.EqualTo(6));
        }

        [Test]
        public void SymmetricTwoKeyDictionary_Marginalizer_Works()
        {
            var result = dict["a"];
            Assert.That(result["a"], Is.EqualTo(1));
            Assert.That(result["b"], Is.EqualTo(2));
            Assert.That(result["c"], Is.EqualTo(3));
        }

        [Test]
        public void SymmetricTwoKeyDictionary_AddIndex_Works()
        {
            dict["d","a"] = 3;
            Assert.That(dict["a", "d"], Is.EqualTo(3));
            Assert.That(dict["b", "d"], Is.EqualTo(0));
            Assert.That(dict["c", "d"], Is.EqualTo(0));
            Assert.That(dict["d", "d"], Is.EqualTo(0));
        }

        [Test]
        public void SymmetricTwoKeyDictionary_AddTwoIndices_Works()
        {
            dict["d", "e"] = 5;
            Assert.That(dict["d", "a"], Is.EqualTo(0));
            Assert.That(dict["d", "b"], Is.EqualTo(0));
            Assert.That(dict["d", "c"], Is.EqualTo(0));
            Assert.That(dict["d", "d"], Is.EqualTo(0));
            Assert.That(dict["d", "e"], Is.EqualTo(5));
            Assert.That(dict["e", "a"], Is.EqualTo(0));
            Assert.That(dict["e", "b"], Is.EqualTo(0));
            Assert.That(dict["e", "c"], Is.EqualTo(0));
            Assert.That(dict["e", "d"], Is.EqualTo(5));
            Assert.That(dict["e", "e"], Is.EqualTo(0));
        }


        [Test]
        public void SymmetricTwoKeyDictionary_TryGetValue_ReturnsValue()
        {
            int result;

            dict.TryGetValue("a", "c", out result);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void SymmetricTwoKeyDictionary_TryGetValueForInvalidKey_ReturnsDefaultValue()
        {
            int result;

            dict.TryGetValue("z", "z", out result);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SymmetricTwoKeyDictionary_Keys_GivesAllKeyPairings()
        {
            var keys = dict.Keys.ToList();

            Assert.That(keys, Contains.Item("a"));
            Assert.That(keys, Contains.Item("b"));
            Assert.That(keys, Contains.Item("c"));

        }

        [Test]
        public void SymmetricTwoKeyDictionary_Values_GivesAllValues()
        {
            var values = dict.Values.ToList();

            Assert.That(values, Contains.Item(1));
            Assert.That(values, Contains.Item(2));
            Assert.That(values, Contains.Item(3));
            Assert.That(values, Contains.Item(4));
            Assert.That(values, Contains.Item(5));
            Assert.That(values, Contains.Item(6));
        }
    }
}
