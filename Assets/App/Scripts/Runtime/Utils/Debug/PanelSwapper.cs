using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PanelSwapper : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool panelEnable = false;
    [SerializeField] private Vector2 startPosition = Vector2.zero;
    [SerializeField] private Vector2 endPosition = Vector2.zero;
    
    [Header("References")]
    [SerializeField] private RectTransform panelCheatCode;
    
    
    private void Awake()
    {
        if (!panelEnable)
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
