using Unity.VisualScripting;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : Component
{
	private static T instance;
	public static T Instance {
		get
		{
			if (instance == null)
			{
				instance = new GameObject($"Singleton<{typeof(T).Name}>").AddComponent<T>();
			}
			return instance;
		}
		private set 
		{
			instance = value;
		} 
	}

	protected virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
		}
		else
		{
			Debug.LogError($"Singleton<{typeof(T).Name}>: Instance already exists. Destroying duplicate.");
			Destroy(this); // destroy the duplicate
		}
	}
}
