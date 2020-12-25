using System;
using System.Threading;

namespace Mbs.Trading
{
    /// <summary>
    /// A generic thread with in-built auto reset event.
    /// </summary>
    internal class AutoResetEventThread : WaitHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResetEventThread"/> class.
        /// </summary>
        /// <param name="threadStart">The thread start delegate to execute.</param>
        internal AutoResetEventThread(Action threadStart)
        {
            AutoResetEvent = new AutoResetEvent(false);
            SafeWaitHandle = AutoResetEvent.SafeWaitHandle;
            Thread = new Thread(new ThreadStart(threadStart));
        }

        /// <summary>
        /// Gets the auto-reset event.
        /// </summary>
        internal AutoResetEvent AutoResetEvent { get; }

        /// <summary>
        /// Gets the thread.
        /// </summary>
        internal Thread Thread { get; }

        /// <summary>
        /// Implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="explicitDisposing">Disposing.</param>
        protected override void Dispose(bool explicitDisposing)
        {
            if (explicitDisposing)
            {
                AutoResetEvent.Close();
                SafeWaitHandle = null!;
            }

            base.Dispose(explicitDisposing);
        }
    }
}
