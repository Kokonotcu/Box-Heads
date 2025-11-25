using System.Collections;
using UnityEngine;

public abstract class LateStarter : MonoBehaviour
{
	protected float waitTime = 0.05f; // Default wait time, can be overridden in derived classes

	void Awake()
	{
		StartCoroutine(Later(waitTime));
	}

	IEnumerator Later(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		LateAwake();
	}

	public abstract void LateAwake();
}
