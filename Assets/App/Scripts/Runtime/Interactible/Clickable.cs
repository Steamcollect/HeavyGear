using System;
using UnityEngine;
public class Clickable : MonoBehaviour
{
    public Action onClickDown;
    public Action onLongClickDown;

    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public void OnClickDown()
    {
        onClickDown?.Invoke();
    }

    public void OnLongClickDown()
    {
        onLongClickDown?.Invoke();
    }
}