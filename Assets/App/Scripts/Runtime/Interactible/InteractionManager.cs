using UnityEngine;
public class InteractionManager : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] Camera cam;

    [SerializeField] float longTouchTime;
    float touchScreenTime = 0;

    bool isScreenTouch = false;
    bool iscreenLongTouch = false;

    Touch lastTouch;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            OnTouch();

            isScreenTouch = true;
            lastTouch = Input.GetTouch(0);
        }
        else
        {
            if (isScreenTouch)
            {
                OnTouchUp();
            }
        }
    }

    void OnTouch()
    {
        if (!isScreenTouch)
        {
            OnTouchDown();
        }

        touchScreenTime += Time.deltaTime;

        if(touchScreenTime > longTouchTime && !iscreenLongTouch)
        {
            iscreenLongTouch = true;
            OnLongTouch();
        }
    }

    void OnTouchDown()
    {

    }

    void OnLongTouch()
    {
        TryTouchDownInteraction()?.OnLongClickDown();
    }
    
    void OnTouchUp()
    {
        TryTouchDownInteraction()?.OnClickUp();

        isScreenTouch = false;
        iscreenLongTouch = false;
        touchScreenTime = 0;
    }

    Clickable TryTouchDownInteraction()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(lastTouch.position), Vector2.zero);

        if (hit.collider != null && hit.transform.TryGetComponent(out Clickable obj)) return obj;
        
        return null;
    }
}