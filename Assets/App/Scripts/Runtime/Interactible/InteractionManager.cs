using UnityEngine;
public class InteractionManager : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] Camera cam;

    bool isScreenTouch = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if(!isScreenTouch)
            {
                OnTouchDown();
            }

            OnTouch();

            isScreenTouch = true;
        }
        else
        {
            if (isScreenTouch)
            {
                OnTouchUp();
            }

            isScreenTouch = false;
        }
    }

    void OnTouchDown()
    {
        TryTouchDownInteraction();
    }

    void OnTouch()
    {

    }

    void OnTouchUp()
    {

    }

    void TryTouchDownInteraction()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);

        if (hit.collider != null && hit.transform.TryGetComponent(out Clickable obj))
        {
            obj.OnClickDown();
        }
    }
}