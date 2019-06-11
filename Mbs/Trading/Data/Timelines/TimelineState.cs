namespace Mbs.Trading.Data.Timelines
{
    /// <summary>
    /// The state of a <see cref="Timeline"/>.
    /// </summary>
    public enum TimelineState
    {
        /// <summary>
        /// Stopped.
        /// </summary>
        Stop,

        /// <summary>
        /// Running.
        /// </summary>
        Run,

        /// <summary>
        /// Paused.
        /// </summary>
        Pause,

        /// <summary>
        /// Advancing one step.
        /// </summary>
        Step
    }
}
