using System;

namespace Kleng.Components.Exceptions
{
    /// <summary>
    ///     Exception class throwing when Speech engine is required but not initialized.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0</version>
    [Serializable]
    public class SpeechEngineNotInitializedException : Exception
    {
        /// <summary>
        ///     Base empty constructor.
        /// </summary>
        public SpeechEngineNotInitializedException()
        {
        }

        /// <summary>
        ///     Base constructor with message.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public SpeechEngineNotInitializedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Base constructor with message and a inner exception.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="inner">Inner exception.</param>
        public SpeechEngineNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}