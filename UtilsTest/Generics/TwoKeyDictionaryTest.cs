/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using NUnit.Framework;
using Lospi.Utils.Generics;
using System.Collections.Generic;

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
        public void TwoKeyDictionaryTest_Getters_EqualSetters()
        {
            Assert.That(dict["b", 2], Is.EqualTo(5D));
        }

        [Test]
        public void TwoKeyDictionaryTest_KeyOneMarginalizer_Works()
        {
            var result = dict.MarginalizeKeyOne("b");
            Assert.That(result[1], Is.EqualTo(4D));
            Assert.That(result[2], Is.EqualTo(5D));
            Assert.That(result[3], Is.EqualTo(6D));
        }

        [Test]
        public void TwoKeyDictionaryTest_KeyTwoMarginalizer_Works()
        {
            var result = dict.MarginalizeKeyTwo(3);
            Assert.That(result["a"], Is.EqualTo(3D));
            Assert.That(result["b"], Is.EqualTo(6D));
        }

        [Test]
        public void TwoKeyDictionaryTest_AddSecondIndex_Works()
        {
            dict["b", 0] = 1D;
            Assert.That(dict["b", 0], Is.EqualTo(1D));
            Assert.That(dict["a", 0], Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionaryTest_AddFirstIndex_Works()
        {
            dict["c", 1] = 2D;
            Assert.That(dict["c", 1], Is.EqualTo(2D));
            Assert.That(dict["c", 2], Is.EqualTo(0D));
            Assert.That(dict["c", 3], Is.EqualTo(0D));
        }

        [Test]
        public void TwoKeyDictionaryTest_AddFirstAndSecondIndex_Works()
        {
            dict["c", 0] = 2D;
            Assert.That(dict["a", 0], Is.EqualTo(0D));
            Assert.That(dict["b", 0], Is.EqualTo(0D));
            Assert.That(dict["c", 0], Is.EqualTo(2D));
            Assert.That(dict["c", 1], Is.EqualTo(0D));
            Assert.That(dict["c", 2], Is.EqualTo(0D));
            Assert.That(dict["c", 3], Is.EqualTo(0D));
        }
    }
}
