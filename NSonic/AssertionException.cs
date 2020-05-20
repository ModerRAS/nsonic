﻿using System;

namespace NSonic
{
    /// <summary>
    /// Thrown when an assertion exception occurs within NSonic.
    /// </summary>
    public class AssertionException : Exception
    {
        /// <summary>
        /// Constructs a new assertion exception with the given message.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="actual">Actual value.</param>
        internal AssertionException(string message, string actual)
            : base(message + (string.IsNullOrEmpty(actual) ? "" : $" : {actual}"))
        {
            //
        }
    }
}
