using UnityEngine;
public class InteractionManager : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] Camera cam;

    bool isScreenTouch = false;

    float touchScreenTime = 0;
    [SerializeField] float longTouchTime;

    bool iscreenLongTouch = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            OnTouch();

            isScreenTouch = true;
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
        TryTouchDownInteraction()?.OnClickDown();
    }

    void OnLongTouch()
    {
        TryTouchDownInteraction()?.OnLongClickDown();
    }
    
    void OnTouchUp()
    {
        isScreenTouch = false;
        iscreenLongTouch = false;
        touchScreenTime = 0;
    }

    Clickable TryTouchDownInteraction()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);

        if (hit.collider != null && hit.transform.TryGetComponent(out Clickable obj)) return obj;
        
        return null;
    }
}