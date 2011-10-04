/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using NUnit.Framework;
using Lospi.Utils.Generics;
using System.Collections.Generic;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class SealedSymmetricTwoKeyDictionaryTest
    {
        SealedSymmetricTwoKeyDictionary<string, int> dict;

        [SetUp]
        public void TwoKeyDictionaryTestSetUp()
        {
            string[] keys = { "a", "b", "c"};

            dict = new SealedSymmetricTwoKeyDictionary<string, int>(keys);

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
        public void TwoKeyDictionaryTest_Getters_EqualSetters()
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
        public void TwoKeyDictionaryTest_Marginalizer_Works()
        {
            var result = dict["a"];
            Assert.That(result["a"], Is.EqualTo(1));
            Assert.That(result["b"], Is.EqualTo(2));
            Assert.That(result["c"], Is.EqualTo(3));
        }

    }
}
