using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> This script serves as a timer for necessary operations </summary>
/// <summary> No game object attached <summary>


public class Timer : MonoBehaviour
{
	#region Properties
	private float totalSeconds = 0;
	private float elapsedSeconds = 0;
	private bool running = false;	
	private bool started = false;
	#endregion
	
	#region GettersAndSetters
	// Sets the duration of the timer
	// The duration can only be set if the timer isn't currently running
	public float Duration
    {
		set
        {
			if (!running)
            {
				totalSeconds = value;
			}
		}
	}
	
	// Gets whether or not the timer has finished running
	// This property returns false if the timer has never been started
	public bool Finished
    {
		get { return started && !running; } 
	}
	
	/// Gets whether or not the timer is currently running
	public bool Running
    {
		get { return running; }
	}
    #endregion


    #region Methods
    void Update()
    {	
		// update timer and check for finished
		if (running)
        {
			elapsedSeconds += Time.deltaTime;
			if (elapsedSeconds >= totalSeconds)
            {
				running = false;
			}
		}
	}
	/// Runs the timer
	/// Because a timer of 0 duration doesn't really make sense,
	/// the timer only runs if the total seconds is larger than 0
	/// This also makes sure the consumer of the class has actually 
	/// set the duration to something higher than 0
	public void Run()
    {	
		// only run with valid duration
		if (totalSeconds > 0)
        {
			started = true;
			running = true;
            elapsedSeconds = 0;
		}
	}
	#endregion
}
