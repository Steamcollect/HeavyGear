using System;
using UnityEngine;
public class Clickable : MonoBehaviour
{
    public Action onClickUp;
    public Action onLongClickDown;

    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public void OnClickUp()
    {
        Debug.Log("Click");
        onClickUp?.Invoke();
    }

    public void OnLongClickDown()
    {
        onLongClickDown?.Invoke();
    }
}