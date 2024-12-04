using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler
{
    [Header("Resources")]
    [SerializeField] private RectTransform swayObject;
    [SerializeField] private CanvasGroup normal;
    [SerializeField] private CanvasGroup pressed;

    [Header("Settings")]
    [SerializeField] private float transitionSpeed;

    [Header("Events")]
    [SerializeField] private UnityEvent onClick;

    private void Awake()
    {
        normal.alpha = 1;
        pressed.alpha = 0;
    }

    public void OnPointerClick(PointerEventData data)
    {
        StopCoroutine("ButtonPressed");
        StartCoroutine("ButtonPressed");
        onClick.Invoke();
    }

    private IEnumerator ButtonPressed()
    {
        pressed.gameObject.SetActive(true);

        while(swayObject.localScale.x > 0.8f)
        {
            pressed.alpha += Time.unscaledDeltaTime * transitionSpeed;

            swayObject.localScale -= Vector3.one * Time.unscaledDeltaTime * (transitionSpeed / 10);

            yield return null;
        }

        pressed.alpha = 1;
        swayObject.localScale = Vector3.one * 0.8f;

        while (swayObject.localScale.x < 1.0f)
        {
            pressed.alpha -= Time.unscaledDeltaTime * transitionSpeed;

            swayObject.localScale += Vector3.one * Time.unscaledDeltaTime * (transitionSpeed / 10);

            yield return null;
        }

        pressed.alpha = 0;
        swayObject.localScale = Vector3.one;

        pressed.gameObject.SetActive(false);
    }
}