using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler
{
    [Header("References")]
    [SerializeField] private RectTransform swayObject;
    [SerializeField] private CanvasGroup normal;
    [SerializeField] private CanvasGroup pressed;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;

    [Header("Events")]
    [SerializeField] private UnityEvent onClick;

    private void Awake()
    {
        normal.alpha = 1;
        pressed.alpha = 0;
    }

    public void OnPointerDown(PointerEventData data)
    {
        animator.Play(clip.name);
        onClick.Invoke();
    }
}