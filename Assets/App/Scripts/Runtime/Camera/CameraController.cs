using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float movementTime;

    public Vector2 posOffset;

    Vector3 dragStartPosition, dragCurrentPosition;
    Vector3 newPosition;

    int lastTouchCount;

    [Space(10)]
    [SerializeField] Vector2 horizontalWallPosition;
    [SerializeField] Vector2 verticalWallPosition;

    [Header("References")]
    [SerializeField] Camera cam;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Start()
    {
        dragStartPosition = transform.position;
        dragCurrentPosition = transform.position;
    }

    private void Update()
    {
        CalculateMovement();
        Move();
    }

    void CalculateMovement()
    {
        if(Input.touchCount > 0)
        {
            if (lastTouchCount == 0)
            {
                dragStartPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                dragCurrentPosition = dragStartPosition;
            }
            else
            {
                dragCurrentPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        lastTouchCount = Input.touchCount;
    }
    void Move()
    {
        newPosition.z = -10;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }

    Vector2 HorizontalBounds()
    {
        float min = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        float max = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

        return new Vector2(min, max);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(horizontalWallPosition.x, verticalWallPosition.x), new Vector2(horizontalWallPosition.x, verticalWallPosition.y));
        Gizmos.DrawLine(new Vector2(horizontalWallPosition.y, verticalWallPosition.x), new Vector2(horizontalWallPosition.y, verticalWallPosition.y));

        Gizmos.DrawLine(new Vector2(horizontalWallPosition.x, verticalWallPosition.x), new Vector2(horizontalWallPosition.y, verticalWallPosition.x));
        Gizmos.DrawLine(new Vector2(horizontalWallPosition.x, verticalWallPosition.y), new Vector2(horizontalWallPosition.y, verticalWallPosition.y));
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(HorizontalBounds().x, 0), new Vector2(HorizontalBounds().y, 0));
    }
}