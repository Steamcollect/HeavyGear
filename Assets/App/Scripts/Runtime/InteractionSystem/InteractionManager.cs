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

    bool canInteract = true;

    [Header("Input")]
    [SerializeField] RSE_LockWorldInteraction rseLockInteraction;
    [SerializeField] RSE_UnlockWorldInteraction rseUnlockInteraction;

    private void OnEnable()
    {
        rseLockInteraction.action += LockCamera;
        rseUnlockInteraction.action += UnlockCamera;
    }
    private void OnDisable()
    {
        rseLockInteraction.action -= LockCamera;
        rseUnlockInteraction.action -= UnlockCamera;
    }

    private void Update()
    {
        if(!canInteract) return;

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
        Debug.Log("qdqdqsdqs");
        Ray ray = cam.ScreenPointToRay(lastTouch.position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("qsd");
            if (hit.collider != null && hit.transform.TryGetComponent(out Clickable obj)) return obj;
        }
        
        return null;
    }

    void LockCamera()
    {
        canInteract = false;
    }
    void UnlockCamera()
    {
        canInteract = true;
    }
}