using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float itemSpacing;

    [Header("References")]
    [SerializeField] Transform[] pathPoints;

    [SerializeField] Transform[] debugItems;

    List<ConveyorBeltItem> items = new List<ConveyorBeltItem>();

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Start()
    {
        foreach (var item in debugItems) AddItem(item);
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
                if (items[i].startPoint < pathPoints.Length - 2)
                {
                    items[i].currentLerp = 0;
                    items[i].startPoint++;
                    items[i].pathDistance = Vector3.Distance(pathPoints[items[i].startPoint].position, pathPoints[items[i].startPoint + 1].position);
                }
            }

            // Check if current item to close frome the front item
            if(i > 0 && Vector3.Distance(items[i].item.position, items[i - 1].item.position) <= itemSpacing)
            {
                continue;
            }

            // Move current item along the current line
            items[i].item.position = Vector3.Lerp(pathPoints[items[i].startPoint].position, pathPoints[items[i].startPoint + 1].position, items[i].currentLerp);
            items[i].currentLerp += (moveSpeed * Time.deltaTime) / items[i].pathDistance;
        }
    }

    void AddItem(Transform item)
    {
        items.Add(new ConveyorBeltItem(item));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if(pathPoints.Length > 0)
        {
            for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
            }
        }        
    }
}