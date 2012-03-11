/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Lospi.Utils;
using Lospi.Utils.Generics;
using NUnit.Framework;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class CopyExtensionMethodsTest
    {
        internal class TestCopyValue : IDeepCopyable<TestCopyValue>, IComparable<TestCopyValue>
        {
            public string Value { get; set; }

            public TestCopyValue DeepCopy()
            {
                return new TestCopyValue { Value = Value };
            }

            public int CompareTo(TestCopyValue other)
            {
                return other.Value.CompareTo(Value);
            }

            public override bool Equals(object obj)
            {
                if(obj.GetType().Equals(GetType()))
                {
                    return ((TestCopyValue) obj).Value.Equals(Value);
                }
                return false;
            }

            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }
        }

        [Test]
        public void DeepMemberwiseCopyOfTestCopyValueContainsIdenticalValues()
        {
            IList<TestCopyValue> baseList = new List<TestCopyValue>();
            const int count = 10;

            for (int i = 0; i < count; i++)
            {
                baseList.Add(new TestCopyValue { Value = i.ToString() });
            }

            IList<TestCopyValue> deepCopy = baseList.DeepMemberwiseCopy().ToList();

            for (int i = 0; i < count; i++)
            {
                Assert.That(baseList[i].Value, Is.EqualTo( deepCopy[i].Value));
            }
        }

        [Test]
        public void DeepMemberwiseCopyOfTestCopyValueIsIndependent()
        {
            IList<TestCopyValue> baseList = new List<TestCopyValue>();
            const int count = 10;

            for (var i = 0; i < count; i++)
            {
                baseList.Add(new TestCopyValue { Value = i.ToString() });
            }

            IList<TestCopyValue> deepCopy = baseList.DeepMemberwiseCopy().ToList();

            for (var i = 0; i < count; i++)
            {
                baseList[i].Value = "CLEARED";
            }

            for (var i = 0; i < count; i++)
            {
                Assert.That(baseList[i].Value, Is.Not.EqualTo(deepCopy[i].Value));
            }
        }

        [Test]
        public void DictionaryDeepCopyContainsIdenticalValues()
        {
            var dictionary = new Dictionary<TestCopyValue, int>();
            const int count = 10;

            for (var i = 0; i < count; i++)
            {
                dictionary.Add(new TestCopyValue {Value = i.ToString()}, i);
            }

            var deepCopy = dictionary.DeepCopyToDictionary();

            foreach(var key in dictionary.Keys)
            {
                Assert.That(dictionary[key], Is.EqualTo( deepCopy[key]));
            }
        }

        [Test]
        public void DeepMemberwiseCopyOfDictionaryIsIndependent()
        {
            var dictionary = new Dictionary<TestCopyValue, int>();
            const int count = 10;

            for (var i = 0; i < count; i++)
            {
                dictionary.Add(new TestCopyValue { Value = i.ToString() }, i);
            }

            var deepCopy = dictionary.DeepCopyToDictionary();

            foreach (var key in dictionary.Keys.ToList())
            {
                dictionary[key] = -99;
            }

            foreach (var key in dictionary.Keys)
            {
                Assert.That(dictionary[key], Is.Not.EqualTo(deepCopy[key]));
            }
        }
    }
}
