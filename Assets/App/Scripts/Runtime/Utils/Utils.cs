using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class Utils
{
    #region IENUMERABLE
    public static T GetRandom<T>(this IEnumerable<T> elems)
    {
        if (elems.Count() == 0)
        {
            Debug.LogError("Try to get random elem from empty IEnumerable");
        }
        return elems.ElementAt(new System.Random().Next(0, elems.Count()));
    }

    /// <summary>
    /// Return all the position in IEnumarable of transform
    /// </summary>
    /// <param name="elems"></param>
    /// <returns></returns>
    public static IEnumerable<Vector3> GetAllPosition(this IEnumerable<Transform> elems)
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (Transform t in elems)
        {
            positions.Add(t.position);
        }

        return positions;
    }
    #endregion

    #region COROUTINE
    public static IEnumerator Delay(float delay, Action ev)
    {
        yield return new WaitForSeconds(delay);
        ev?.Invoke();
    }
    #endregion

    #region SCENE
    /// <summary>
    /// Load Scene Asyncronely and call action at the end
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <param name="loadMode"></param>
    /// <param name="callbackRealised"></param>
    /// <returns></returns>
    public static IEnumerator LoadSceneAsync(int sceneIndex, LoadSceneMode loadMode, Action callbackRealised)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, loadMode);

        yield return new WaitUntil(() => asyncLoad is { isDone: true });

        callbackRealised.Invoke();
    }
    public static IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadMode, Action callbackDone)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, loadMode);

        yield return new WaitUntil(() => asyncLoad is { isDone: true });
        callbackDone.Invoke();
    }

    /// <summary>
    /// Unload Scene Asyncronely and call action at the end
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <param name="sceneName"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerator UnloadSceneAsync(string sceneName, Action action)
    {
        if (SceneManager.loadedSceneCount > 1)
        {
            AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneName);

            yield return new WaitUntil(() => asyncLoad is { isDone: true });
        }

        action.Invoke();
    }
    #endregion

    public static bool NumberInRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }
    
}