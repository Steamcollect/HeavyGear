using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WindowManager : MonoBehaviour
{
    public List<WindowItem> windows = new List<WindowItem>();

    public int currentWindowIndex = 0;
    private int currentButtonIndex = 0;
    private int newWindowIndex;

    [System.Serializable] public class WindowChangeEvent : UnityEvent<int> { }
    public WindowChangeEvent onWindowChange;

    private GameObject currentWindow;
    private GameObject nextWindow;
    private GameObject currentButton;
    private GameObject nextButton;
    private Animator currentWindowAnimator;
    private Animator nextWindowAnimator;
    private Animator currentButtonAnimator;
    private Animator nextButtonAnimator;

    string windowFadeIn = "In";
    string windowFadeOut = "Out";
    string buttonFadeIn = "Hover to Pressed";
    string buttonFadeOut = "Pressed to Normal";
    float cachedStateLength;
    public bool altMode;

    [System.Serializable]
    public class WindowItem
    {
        [BoxGroup] public string windowName = "My Window";
        [BoxGroup] public GameObject windowObject;
        [BoxGroup] public GameObject buttonObject;
        [BoxGroup] public GameObject firstSelected;
    }

    void Awake()
    {
        if (windows.Count == 0)
            return;

        InitializeWindows();
    }

    public void InitializeWindows()
    {
        if (windows[currentWindowIndex].firstSelected != null)
        { 
            EventSystem.current.firstSelectedGameObject = windows[currentWindowIndex].firstSelected;
        }
        if (windows[currentWindowIndex].buttonObject != null)
        {
            currentButton = windows[currentWindowIndex].buttonObject;
            currentButtonAnimator = currentButton.GetComponent<Animator>();
            currentButtonAnimator.Play(buttonFadeIn);
        }
    }
}