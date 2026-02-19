using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedEvent : Singleton<DelayedEvent>
{
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    public void Wait(Action action, float waitTime)
    {
        StartCoroutine(Waiter(action, waitTime));
    }

    private IEnumerator Waiter(Action action, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        try
        {
            action?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void OnSceneChanged(Scene s1, Scene s2)
    {
        if (Application.isPlaying)
        {
            StopAllCoroutines();
        }
    }
}
