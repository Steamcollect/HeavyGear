using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCheatCode : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool debugEnabled = false;
    [SerializeField] private Vector2 startPosition = Vector2.zero;
    [SerializeField] private Vector2 endPosition = Vector2.zero;
    
    [Header("References")]
    [SerializeField] private RectTransform panelCheatCode;
    
    
    private void Awake()
    {
        if (!debugEnabled)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        panelCheatCode.anchoredPosition = startPosition;
    }
    
    public void SwapPanel()
    {
        panelCheatCode.anchoredPosition = panelCheatCode.anchoredPosition == endPosition ? startPosition : endPosition;
    }
    
    
}
