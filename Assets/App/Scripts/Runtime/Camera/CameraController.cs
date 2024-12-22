using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float detectionDistance;
    [SerializeField] float movementTime;
    [SerializeField] float friction;
    float movementMultiplier;
    bool canMove = true;
    bool isMoving;

    Vector3 dragStartPosition, dragCurrentPosition;
    Vector3 newPosition;

    int lastTouchCount;

    //[SerializeField] float zoomMultplier;
    //[SerializeField] Vector2 minMaxZoom;
    //float startingDistanceBetweenFingers, distanceBetweenFingers;

    [Space(10)]
    [SerializeField] Vector2 horizontalWallPosition;
    [SerializeField] Vector2 verticalWallPosition;

    [Header("References")]
    [SerializeField] Camera cam;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_LockCamera rseLockCamera;
    [SerializeField] RSE_UnlockCamera rseUnlockCamera;

    //[Header("Output")]

    private void OnEnable()
    {
        rseLockCamera.action += LockCamera;
        rseUnlockCamera.action += UnlockCamera;
    }
    private void OnDisable()
    {
        rseLockCamera.action -= LockCamera;
        rseUnlockCamera.action -= UnlockCamera;
    }

    private void Start()
    {
        dragStartPosition = transform.position;
        dragCurrentPosition = transform.position;
    }

    private void Update()
    {
        if (canMove)
        {
            CalculateMovement();
            if (isMoving) Move();
        }
    }

    void CalculateMovement()
    {
        if(Input.touchCount > 0)
        {
            if (lastTouchCount == 0)
            {
                dragStartPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                dragCurrentPosition = dragStartPosition;
                movementMultiplier = 1;
            }
            else
            {
                dragCurrentPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position);

                isMoving = Vector2.Distance(dragStartPosition, dragCurrentPosition) > detectionDistance;
            }
        }
        else
        {
            movementMultiplier -= Time.deltaTime * friction;
            if(movementMultiplier < 0) movementMultiplier = 0;
        }

        lastTouchCount = Input.touchCount;
    }
    void Move()
    {
        newPosition = transform.position + dragStartPosition - dragCurrentPosition;

        // Check if out of box
        if (newPosition.x < horizontalWallPosition.x) newPosition.x = horizontalWallPosition.x;
        if (newPosition.x > horizontalWallPosition.y) newPosition.x = horizontalWallPosition.y;
        if (newPosition.y < verticalWallPosition.x) newPosition.y = verticalWallPosition.x;
        if (newPosition.y > verticalWallPosition.y) newPosition.y = verticalWallPosition.y;

        newPosition.z = -10;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime * movementMultiplier);
    }

    void LockCamera()
    {
        canMove = false;
    }
    void UnlockCamera()
    {
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(horizontalWallPosition.x, verticalWallPosition.x), new Vector2(horizontalWallPosition.x, verticalWallPosition.y));
        Gizmos.DrawLine(new Vector2(horizontalWallPosition.y, verticalWallPosition.x), new Vector2(horizontalWallPosition.y, verticalWallPosition.y));

        Gizmos.DrawLine(new Vector2(horizontalWallPosition.x, verticalWallPosition.x), new Vector2(horizontalWallPosition.y, verticalWallPosition.x));
        Gizmos.DrawLine(new Vector2(horizontalWallPosition.x, verticalWallPosition.y), new Vector2(horizontalWallPosition.y, verticalWallPosition.y));
    }
}