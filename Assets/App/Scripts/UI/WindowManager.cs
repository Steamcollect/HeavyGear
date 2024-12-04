using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindowManager : MonoBehaviour
{
    public List<WindowItem> windows = new List<WindowItem>();
    private int currentWindowIndex = 0;

    private int newWindowIndex;

    public UnityEvent<int> onWindowChange;

    private GameObject currentWindow;
    private GameObject nextWindow;

    float cachedStateLength;
    public bool altMode;

    [System.Serializable]
    public class WindowItem
    {
        [BoxGroup] public string windowName = "My Window";
        [BoxGroup] public GameObject windowObject;
    }

    void Awake()
    {
        if (windows.Count == 0)
            return;

        InitializeWindows();
    }

    public void InitializeWindows()
    {
        currentWindow = windows[currentWindowIndex].windowObject;

        onWindowChange.Invoke(currentWindowIndex);

        for (int i = 0; i < windows.Count; i++)
        {
            if (i != currentWindowIndex)
            {
                windows[i].windowObject.SetActive(false);
            }
        }
    }

    public void OpenWindow(string newWindow)
    {
        for (int i = 0; i < windows.Count; i++)
        {
            if (windows[i].windowName == newWindow)
            {
                newWindowIndex = i;
                break;
            }
        }

        if (newWindowIndex != currentWindowIndex)
        {
            StopCoroutine("DisablePreviousWindow");

            currentWindow = windows[currentWindowIndex].windowObject;

            currentWindowIndex = newWindowIndex;
            nextWindow = windows[currentWindowIndex].windowObject;
            nextWindow.SetActive(true);

            StartCoroutine("DisablePreviousWindow");

            onWindowChange.Invoke(currentWindowIndex);
        }
    }

    public void OpenPanel(string newPanel)
    {
        OpenWindow(newPanel);
    }

    IEnumerator DisablePreviousWindow()
    {
        yield return new WaitForSecondsRealtime(cachedStateLength);

        for (int i = 0; i < windows.Count; i++)
        {
            if (i == currentWindowIndex)
                continue;

            windows[i].windowObject.SetActive(false);
        }
    }
}