/*
 * Copyright © 2011, Joshua A. Lospinoso (josh@lospi.net). All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Lospi.Utils;
using Lospi.Utils.Generics;

namespace Lospi.Test.Utils.Generics
{
    [TestFixture]
    public class CopyExtensionMethodsTest
    {
        internal class TestCopyValue : IDeepCopyable<TestCopyValue>
        {
            public string Value { get; set; }

            public TestCopyValue DeepCopy()
            {
                return new TestCopyValue { Value = this.Value };
            }
        }

        [Test]
        public void DeepMemberwiseCopy_OfTestCopyValue_ContainsIdenticalValues()
        {
            IList<TestCopyValue> baseList = new List<TestCopyValue>();
            int count = 10;

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
        public void DeepMemberwiseCopy_OfTestCopyValue_IsIndependent()
        {
            IList<TestCopyValue> baseList = new List<TestCopyValue>();
            int count = 10;

            for (int i = 0; i < count; i++)
            {
                baseList.Add(new TestCopyValue { Value = i.ToString() });
            }

            IList<TestCopyValue> deepCopy = baseList.DeepMemberwiseCopy().ToList();

            for (int i = 0; i < count; i++)
            {
                baseList[i].Value = "CLEARED";
            }

            for (int i = 0; i < count; i++)
            {
                Assert.That(baseList[i].Value, Is.Not.EqualTo(deepCopy[i].Value));
            }
        }
    }
}
