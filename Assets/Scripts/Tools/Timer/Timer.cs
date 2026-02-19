using UnityEngine;
using System;
using System.Collections.Generic;

public class Timer : Singleton<Timer>
{
	public class TimerData
	{
		private bool hasTriggered = false;
		public bool HasTriggered
		{
			get
			{
				bool _trig = hasTriggered; 
				hasTriggered = false; 
				if (_trig)
				{
					elapsedTime = 0.0f;
				} 
				return _trig;  
			}
			set { hasTriggered = value; }
		}
		public float frequency;
		public float elapsedTime;
		public Action callback; //Optional callback to be invoked when the timer reaches its frequency
	}

	List<TimerData> timers = new List<TimerData>();

    private void OnDisable()
    {
        if (timers.Count > 0)
		{
			timers.Clear();
        }
    }

    public TimerData RequestTimer(float frequency, Action callback = null)
	{
		if (frequency <= 0)
		{
			Debug.LogError("Timer frequency must be greater than zero.");
			return null;
		}
		timers.Add(new TimerData
		{
			frequency = frequency,
			elapsedTime = 0f,
			callback = callback
		});
		return timers[timers.Count - 1]; // Return the newly created timer
	}

	public void DeleteTimer(TimerData data) 
	{
		timers.Remove(data);
	}

	private void Update()
	{
		for (int i = 0; i < timers.Count; i++)
		{
			timers[i].elapsedTime += Time.deltaTime;
			if (timers[i].elapsedTime >= timers[i].frequency)
			{
				timers[i].elapsedTime = 0.0f; // Reset elapsed time
				timers[i].callback?.Invoke(); // Invoke the callback if it exists
				timers[i].HasTriggered = true; // Mark as triggered
			}
		}
	}
}
