using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float movementTime;
    bool canMove;

    Vector3 dragStartPosition, dragCurrentPosition;
    Vector3 newPosition;

    int lastTouchCount;

    [SerializeField] float zoomMultplier;
    [SerializeField] Vector2 minMaxZoom;
    float startingDistanceBetweenFingers, distanceBetweenFingers;

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
        if(canMove) Move();
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

                canMove = Vector2.Distance(dragStartPosition, dragCurrentPosition) > 1 ;
            }

            if(Input.touchCount == 2 && lastTouchCount != 2)
            {
                if(lastTouchCount != 2)
                {
                    startingDistanceBetweenFingers = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                }
                else
                {
                    distanceBetweenFingers = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                }

                float newZoom = (distanceBetweenFingers - startingDistanceBetweenFingers) * zoomMultplier;
                if(newZoom < minMaxZoom.x) newZoom = minMaxZoom.x;
                if (newZoom > minMaxZoom.y) newZoom = minMaxZoom.y;
                cam.orthographicSize = newZoom;
            }
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
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
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