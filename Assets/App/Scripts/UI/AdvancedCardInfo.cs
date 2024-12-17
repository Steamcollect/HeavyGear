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

    public void ShowAdvancedCard(GameObject baseCard)
    {
        RectTransform targetRectTransform = rectTransform;

        rectTransform = baseCard.GetComponent<RectTransform>();
        baseCard.GetComponent<CanvasGroup>().alpha = 0.0f;
        canvasGroup.alpha = 1.0f;

        StartCoroutine("UpgradeCardSize");
    }

    public void CloseAdvancedCard(GameObject baseCard)
    {

    }

    IEnumerable UpgradeCardSize()
    {
        yield return new WaitForSeconds(0.5f);
    }
}