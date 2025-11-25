using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler 
{
	private readonly GameObject prefab;
	private readonly Transform parent;
	private List<GameObject> pool;
	private Stack<int> availables;

	public ObjectPooler(GameObject _prefab, Transform _parent = null, int initialSize = 10)
	{
		prefab = _prefab;
		parent = _parent;
		pool = new List<GameObject>(initialSize);
		availables = new Stack<int>(initialSize);

		for (int i = 0; i < initialSize; i++)
		{
			GameObject obj;
			if (parent != null) 
			{ 
				obj = UnityEngine.Object.Instantiate(prefab, parent.position,parent.rotation,parent); 
			}
			else
			{
				obj = UnityEngine.Object.Instantiate(prefab);
			}

			availables.Push(i);
			obj.SetActive(false);
			pool.Add(obj);
		}
	}

	public GameObject Spawn(Vector3 position = default, Quaternion rotation = default) 
	{
		GameObject newObj;
		if (availables.Count> 0)
		{
			int index = availables.Pop();
			newObj = pool[index];
			pool.RemoveAt(index);
			newObj.SetActive(true);
			newObj.transform.position = position;
			newObj.transform.rotation = rotation;
			return newObj;
		}
		else
		{
			
			if (parent != null)
				newObj = UnityEngine.Object.Instantiate(prefab, position, rotation, parent);
				
			else
				newObj = UnityEngine.Object.Instantiate(prefab, position, rotation);

			return newObj;
		}
	}

	public GameObject Spawn(Vector3 position = default, Vector3 direction = default)
	{
		GameObject newObj;
		if (availables.Count > 0)
		{
			int index = availables.Pop();
			newObj = pool[index];
			pool.RemoveAt(index);
			newObj.SetActive(true);
			newObj.transform.position = position;
			newObj.transform.rotation = Quaternion.LookRotation(direction);
			return newObj;
		}
		else
		{

			if (parent != null)
				newObj = UnityEngine.Object.Instantiate(prefab, position, Quaternion.LookRotation(direction), parent);

			else
				newObj = UnityEngine.Object.Instantiate(prefab, position, Quaternion.LookRotation(direction));

			return newObj;
		}
	}

	public GameObject Spawn(Transform root)
	{
		GameObject newObj;
		if (availables.Count > 0)
		{
			int index = availables.Pop();
			newObj = pool[index];
			pool.RemoveAt(index);
			newObj.SetActive(true);
			newObj.transform.position = root.position;
			newObj.transform.rotation = root.rotation;
			return newObj;
		}
		else
		{
			if (parent != null)
				newObj = UnityEngine.Object.Instantiate(prefab, root.position, root.rotation, parent);
			else
				newObj = UnityEngine.Object.Instantiate(prefab, root.position, root.rotation);
			
			return newObj;
		}
	}

	public void Release(GameObject obj) 
	{
		pool.Add(obj);
		obj.SetActive(false);
		int index = pool.Count - 1;
		availables.Push(index);
	}

	public void Clear()
	{
		foreach (var obj in pool)
		{
			if (obj != null)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
		pool.Clear();
		availables.Clear();
	}
}
