using UnityEngine;

namespace Utilities.Timers
{
    /// <summary>
    /// Simple timer
    /// </summary>
    public class Timer
    {
        #region Properties
        /// <summary>
	    /// Get elapsed seconds since the timer runs
	    /// </summary>
	    public float ElapsedSeconds { get; protected set; }
        /// <summary>
        /// Gets whether the timer was running
        /// </summary>
        /// <value>true if running; otherwise, false.</value>
        public bool Running { get; protected set; }
        #endregion

        #region Methods
        /// <summary>
        /// Update the timer. It is suggested you called once per frame
        /// </summary>
        public void Update()
        {
            if(Running)
                ElapsedSeconds += Time.deltaTime;
        }

        /// <summary>
        /// Runs the timer
        /// </summary>
        public void Run() => Running = true;
        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop() => Running = false;
        /// <summary>
        /// Reset the timer
        /// </summary>
        public void Reset()
        {
            ElapsedSeconds = 0f;
        }
        /// <summary>
        /// Stops and reset the timer
        /// </summary>
        public void StopAndReset()
        {
            Stop();
            Reset();
        }
        #endregion
    }
}