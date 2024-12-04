using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler
{
    [Header("Events")]
    [SerializeField] private UnityEvent onClick;

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Click");
        onClick.Invoke();
    }
}