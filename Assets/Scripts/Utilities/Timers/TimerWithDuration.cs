using System;
using UnityEngine;

/// <summary>
/// Timer running for a certain duration
/// </summary>
public class TimerWithDuration
{
    #region Properties
    /// <summary>
    /// Gets the duration of the timer
    /// </summary>
    public float Duration { get; protected set; }
    /// <summary>
    /// Gets whether or not the timer has finished running
    /// This property returns false if the timer has never been started
    /// </summary>
    /// <value>true if finished; otherwise, false.</value>
    public bool Finished { get; protected set; }
    /// <summary>
    /// Gets the seconds that have passed since the timer runs
    /// </summary>
    public bool Running { get; protected set; }
    /// <summary>
    /// Gets whether the timer was running
    /// </summary>
    /// <value>true if running; otherwise, false.</value>
    public float ElapsedSeconds { get; protected set; }
    #endregion

    #region Methods
    /// <summary>
    /// Update the timer. It is suggested you called once per frame
    /// </summary>
    public void Update()
    {
        if (Running)
        {
            ElapsedSeconds += Time.deltaTime; // Update timer 
            if (ElapsedSeconds >= Duration) // Check for finished
            {
                Stop();
            }
        }
    }
    /// <summary>
    /// Runs the timer with duration
    /// </summary>
    /// <param name = "duration"> Sets the duration of the timer. Value must be greater than zero</param>
    public void Run(float duration)
    {
        if (duration <= 0f) // only run with valid duration
            throw new ArgumentException("Value must be greater than 0", nameof(duration));
        Duration = duration;
        Running = true;
    }
    /// <summary>
    /// Stops and reset the timer
    /// </summary>
    public void StopAndReset()
    {
        Stop();
        Reset();
    }
    /// <summary>
    /// Stops the timer
    /// </summary>
    private void Stop()
    {
        Running = false;
        Finished = true;
    }
    /// <summary>
    /// Reset the timer
    /// </summary>
    private void Reset()
    {
        Finished = false;
        Duration = ElapsedSeconds = 0f;
    }
    #endregion
}