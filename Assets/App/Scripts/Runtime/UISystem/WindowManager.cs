using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindowManager : MonoBehaviour
{
    [Header("Settings")]
    public List<WindowItem> windows = new List<WindowItem>();

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
        [BoxGroup] public Window window;
    }

    

    private void OnEnable()
    {
        rseOpenWindow.action += OpenWindow;
    }

    private void OnDisable()
    {
        rseOpenWindow.action -= OpenWindow;
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
                windows[i].window.SetWindowActiveFalse();
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
            nextWindow.window.SetWindowActiveTrue();

            if (currentWindow.window != null) currentWindow.window.WindowFadeOut();
            if (nextWindow.window != null) nextWindow.window.WindowFadeIn();

            StartCoroutine("DisablePreviousWindow");
        }
    }

    IEnumerator DisablePreviousWindow()
    {
        yield return new WaitForSecondsRealtime(.2f);

        for (int i = 0; i < windows.Count; i++)
        {
            if (i == currentWindowIndex)
                continue;

            windows[i].window.SetWindowActiveFalse();
        }
    }
}