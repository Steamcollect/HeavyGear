using System;
using UnityEngine;
public class Clickable : MonoBehaviour
{
    public Action onClickUp;
    public Action onLongClickDown;

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