using System;

namespace Kleng.Components.Exceptions
{
    /// <summary>
    ///     Exception class throwing when a wrong activity file is trying to load.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0</version>
    [Serializable]
    public class ActivityFormatException : Exception
    {
        /// <summary>
        ///     Base empty constructor.
        /// </summary>
        public ActivityFormatException()
        {
        }

        /// <summary>
        ///     Base constructor with message.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public ActivityFormatException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Base constructor with message and a inner exception.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="inner">Inner exception.</param>
        public ActivityFormatException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}