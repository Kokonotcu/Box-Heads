using Unity.VisualScripting;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    protected static bool isQuitting = false;

    public static T Instance
    {
        get
        {
            if (instance == null && !isQuitting)
            {
                instance = FindFirstObjectByType<T>();

                if (instance == null)
                {
                    GameObject obj = new GameObject($"Singleton<{typeof(T).Name}>");
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
