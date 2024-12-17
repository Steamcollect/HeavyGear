using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AdvancedCardInfo : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Start()
    {
        canvasGroup.alpha = 0.0f;
    }

    public void ShowAdvancedCard(CardUI card)
    {
        canvasGroup.alpha = 1.0f;
    }

    public void CloseAdvancedCard(CardUI card)
    {
        canvasGroup.alpha = 0.0f;
    }
}