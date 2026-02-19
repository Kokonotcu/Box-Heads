using Unity.VisualScripting;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();

                if (instance == null)
                {
                    instance = new GameObject($"Singleton<{typeof(T).Name}>").AddComponent<T>();
                }
            }
            return instance;
        }
    }
}
