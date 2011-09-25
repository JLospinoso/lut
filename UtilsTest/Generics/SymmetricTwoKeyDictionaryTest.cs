/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using NUnit.Framework;
using Lospi.Utils.Generics;
using System.Collections.Generic;

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

        [Test]
        public void TwoKeyDictionaryTest_AddIndex_Works()
        {
            dict["d","a"] = 3;
            Assert.That(dict["a", "d"], Is.EqualTo(3));
            Assert.That(dict["b", "d"], Is.EqualTo(0));
            Assert.That(dict["c", "d"], Is.EqualTo(0));
            Assert.That(dict["d", "d"], Is.EqualTo(0));
        }

        [Test]
        public void TwoKeyDictionaryTest_AddTwoIndices_Works()
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
    }
}
