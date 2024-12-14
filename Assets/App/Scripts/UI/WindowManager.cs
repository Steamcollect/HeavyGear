using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindowManager : MonoBehaviour
{
    [Header("Settings")]
    public List<WindowItem> windows = new List<WindowItem>();
    public float delayBeforeSetActiveFalse;

    [Header("References")]
    [SerializeField] private RSE_OpenWindow rseOpenWindow;

    private int currentWindowIndex = 0;
    private int newWindowIndex;

    private WindowItem currentWindow;
    private WindowItem nextWindow;

    [System.Serializable]
    public class WindowItem
    {
        [BoxGroup] public string windowName = "My Window";
        [BoxGroup] public GameObject windowObject;
        [BoxGroup] public WindowAnimator windowAnimator;
    }

    void Awake()
    {
        if (windows.Count == 0)
            return;

        InitializeWindows();
    }

    public void InitializeWindows()
    {
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

            currentWindow = windows[currentWindowIndex];

            currentWindowIndex = newWindowIndex;
            nextWindow = windows[currentWindowIndex];
            nextWindow.windowObject.SetActive(true);

            if (currentWindow.windowAnimator != null) currentWindow.windowAnimator.WindowFadeOut();
            if (nextWindow.windowAnimator != null) nextWindow.windowAnimator.WindowFadeIn();

            StartCoroutine("DisablePreviousWindow");
        }
    }

    public void OpenPanel(string newPanel)
    {
        OpenWindow(newPanel);
    }

    IEnumerator DisablePreviousWindow()
    {
        yield return new WaitForSecondsRealtime(delayBeforeSetActiveFalse);

        for (int i = 0; i < windows.Count; i++)
        {
            if (i == currentWindowIndex)
                continue;

            windows[i].windowObject.SetActive(false);
        }
    }
}