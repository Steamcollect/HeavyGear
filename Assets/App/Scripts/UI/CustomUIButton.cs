using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomUIButton : MonoBehaviour, IPointerDownHandler
{
    [Header("References")]
    [SerializeField] private CanvasGroup normal;
    [SerializeField] private CanvasGroup pressed;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;

    [Header("Events")]
    [SerializeField] private UnityEvent onClick;

    private void Awake()
    {
        if(normal != null) normal.alpha = 1;
        if (pressed != null) pressed.alpha = 0;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(animator != null) animator.Play(clip.name);
        onClick.Invoke();
    }
}