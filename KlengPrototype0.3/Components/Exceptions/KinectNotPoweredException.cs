using System;

namespace Kleng.Components.Exceptions
{
    /// <summary>
    ///     Exception class throwing when Kinect isn't connected to a power supply.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0</version>
    [Serializable]
    public class KinectNotPoweredException : Exception
    {
        /// <summary>
        ///     Base empty constructor.
        /// </summary>
        public KinectNotPoweredException()
        {
        }

        /// <summary>
        ///     Base constructor with message.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public KinectNotPoweredException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Base constructor with message and a inner exception.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="inner">Inner exception.</param>
        public KinectNotPoweredException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}