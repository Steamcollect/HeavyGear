using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler
{
    [Header("Resources")]
    [SerializeField] private CanvasGroup normal;
    [SerializeField] private CanvasGroup pressed;

    [Header("Events")]
    [SerializeField] private UnityEvent onClick;

    private void Awake()
    {
        normal.alpha = 1;
        pressed.alpha = 0;
    }

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Click");
        onClick.Invoke();
    }
}