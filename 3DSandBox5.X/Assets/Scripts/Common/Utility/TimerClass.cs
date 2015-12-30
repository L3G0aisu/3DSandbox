using UnityEngine;
using System.Collections;

public class TimerClass {

	public bool isTimerRunning = false;
	private float timeElapsed = 0f;
	private float currentTime = 0f;
	private float lastTime = 0f;
	private float timeScaleFactor = 1.1f; // change this to scale time

	private string timeString;
	private string hours;
	private string minutes;
	private string seconds;
	private string milliseconds;

	private int aHour;
	private int aMinute;
	private int aSecond;
	private int aMillisecond;
	private int temp;
	private int aTime;

	private GameObject callback;

	public void UpdateTimer () {
		// calculate the time elapsed since the last Update ()
		timeElapsed = Mathf.Abs(Time.realtimeSinceStartup - lastTime);

		// if the timer is running, we add the time elapsed to the current time (advancingthe timer)
		if (isTimerRunning)
			currentTime += timeElapsed * timeScaleFactor;

		// store the current time so that we can use it on the next update
		lastTime = Time.realtimeSinceStartup;
	}

	public void StartTimer () {
		isTimerRunning = true;
		lastTime = Time.realtimeSinceStartup;
	}

	public void StopTimer () {
		isTimerRunning = false;
	}

	public void ResetTimer () {
		timeElapsed = 0f;
		currentTime = 0f;
		lastTime = Time.realtimeSinceStartup;
	}

	public string GetFormattedTime () {
		// carry out an update to the timer so it is 'up to date'
		UpdateTimer ();

		aMinute = (int) currentTime / 60;
		aMinute = aMinute % 60;

		aSecond = (int) currentTime % 60;

		aMillisecond = (int) (currentTime * 100) % 100;
		
		temp = (int) aHour;
		hours = temp.ToString();
		if (hours.Length < 2)
			hours = "0" + hours;

		temp = (int) aSecond;
		seconds = temp.ToString();
		if (seconds.Length < 2)
			seconds = "0" + seconds;

		temp = (int) aMinute;
		minutes = temp.ToString();
		if (minutes.Length < 2)
			minutes = "0" + minutes;

		temp = (int) aMillisecond;
		milliseconds = temp.ToString();
		if (milliseconds.Length < 2)
			milliseconds = "0" + milliseconds;

		timeString = hours + ":" + minutes + ":" + seconds + ":" + milliseconds;

		return timeString;
	}

	public int GetTime () {
		return (int) (currentTime);
	}
}
