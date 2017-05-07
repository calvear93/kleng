using System;
using System.Windows.Threading;

namespace Kleng.Components
{
    /// <summary>
    ///     Timer class. Allows to run code triggered by intervals in a new thread.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.3.2</version>
    internal class TimedThreadHandler : IDisposable
    {
        /// <summary>
        ///     Timer running on a separate thread.
        /// </summary>
        private readonly DispatcherTimer _timer;

        /// <summary>
        ///     Create a new thread, calling a function periodically
        ///     by a predefined interval.
        /// </summary>
        /// <param name="interval">Interval to trigger in milliseconds.</param>
        public TimedThreadHandler(int interval)
        {
            // Creates of the new temporized thread.
            _timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(interval)};
        }

        /// <summary>
        ///     Subscribes a function to Tick event from DispatcherTimer.
        ///     Is recommended to use OnIntervalElapsed(object sender, EventArgs e)
        ///     header for listener function.
        /// </summary>
        public EventHandler IntervalElapsed
        {
            set { _timer.Tick += value; }
        }

        #region TIMER

        /// <summary>
        ///     Starts the timer.
        /// </summary>
        public void Start()
        {
            _timer.Start();
        }

        /// <summary>
        ///     Stops the timer.
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
        }

        /// <summary>
        ///     Changes the interval for calling the function.
        /// </summary>
        /// <param name="interval">Interval in milliseconds.</param>
        public void ChangeInterval(int interval)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(interval);
        }

        /// <summary>
        ///     Disposes the event handled.
        /// </summary>
        public void Dispose()
        {
            _timer.Stop();
        }

        #endregion
    }
}