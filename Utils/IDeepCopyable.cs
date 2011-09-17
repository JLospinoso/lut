﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lospi.Utils
{
    /// <summary>
    /// A deep copyable object can be copied into a completely independent copy.
    /// This is useful for multithreaded applications for task local storage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeepCopyable<T>
    {
        /// <summary>
        /// Provides a deep copy.
        /// </summary>
        /// <returns>A deep copy</returns>
        T DeepCopy();
    }
}