using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float itemSpacing;

    [Header("References")]
    [SerializeField] Transform[] conveyorPathPoints;

    [SerializeField] List<ConveyorBeltItem> items = new List<ConveyorBeltItem>();

    LineRenderer path;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Awake()
    {
        InitializePath();
    }

    private void FixedUpdate()
    {
        MoveItems();
    }

    void MoveItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            // If item reach the line
            if (items[i].currentLerp >= 1)
            {
                if (items[i].startPoint < path.positionCount - 2)
                {
                    items[i].currentLerp = 0;
                    items[i].startPoint++;
                    items[i].pathDistance = Vector3.Distance(path.GetPosition(items[i].startPoint), path.GetPosition(items[i].startPoint + 1));
                }
            }

            // Check if current item to close frome the front item
            if(i > 0 && Vector3.Distance(items[i].item.position, items[i - 1].item.position) <= itemSpacing)
            {
                continue;
            }

            // Move current item along the current line
            items[i].item.position = Vector3.Lerp(path.GetPosition(items[i].startPoint), path.GetPosition(items[i].startPoint + 1), items[i].currentLerp);
            items[i].currentLerp += (moveSpeed * Time.deltaTime) / items[i].pathDistance;
        }
    }

    void InitializePath()
    {
        GameObject GO = new GameObject("linePath");
        GO.transform.SetParent(transform);
        path = GO.AddComponent<LineRenderer>();
        path.positionCount = conveyorPathPoints.Length;

        path.SetPositions(conveyorPathPoints.GetAllPosition().ToArray());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < conveyorPathPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(conveyorPathPoints[i].position, conveyorPathPoints[i + 1].position);
        }
    }
}