using System;
using System.Collections;
using UnityEngine;

public class DelayedEvent : Singleton<DelayedEvent>
{
	
	///<summary>Executes the Callback after waitTime seconds</summary>
	public void Wait (Action action, float waitTime)
	{
		StartCoroutine(Waiter(action,waitTime));
	}

	private IEnumerator Waiter(Action action,float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
