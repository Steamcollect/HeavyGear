using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomUIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private Animator animator;

    [Header("Events")]
    [SerializeField] private UnityEvent onClick;

    private bool isCursorHoverButton;

    public void OnPointerDown(PointerEventData data)
    {
        isCursorHoverButton = true;
        PlayDownAnimation();
    }

    public void OnPointerExit(PointerEventData data)
    {
        isCursorHoverButton = false;
        PlayUpAnimation();
    }

    public void OnPointerUp(PointerEventData data)
    {

        if (isCursorHoverButton) onClick.Invoke();

        PlayUpAnimation();
        isCursorHoverButton = false;
    }

    private void PlayDownAnimation() { if (animator != null) animator.Play("Down"); }
    private void PlayUpAnimation() { if (animator != null) animator.Play("Up"); }
}