using System;

namespace Kleng.Components.Exceptions
{
    /// <summary>
    ///     Exception class throwing when the system language pack isn't recognized.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0</version>
    [Serializable]
    public class LanguagePackException : Exception
    {
        /// <summary>
        ///     Base empty constructor.
        /// </summary>
        public LanguagePackException()
        {
        }

        /// <summary>
        ///     Base constructor with message.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public LanguagePackException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Base constructor with message and a inner exception.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="inner">Inner exception.</param>
        public LanguagePackException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}