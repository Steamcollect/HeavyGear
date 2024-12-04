using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float itemSpacing;

    [HideInInspector] public bool haveSpaceToAddItem = true;

    [Header("References")]
    [SerializeField] Transform[] pathPoints;

    List<ConveyorBeltOre> ores = new List<ConveyorBeltOre>();

    public Action<ConveyorBelt> onObjectTouchTheEnd;
    public Action<ConveyorBelt> onConveyorGetSpace;

    [Space(10)]
    // RSO
    [SerializeField] RSO_OreManager rsoOreManager;

    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void FixedUpdate()
    {
        MoveItems();
    }

    void MoveItems()
    {
        if(ores.Count <= 0)
        {
            haveSpaceToAddItem = true;
        }

        for (int i = 0; i < ores.Count; i++)
        {
            // If item reach the end of the line
            if (ores[i].currentLerp >= 1)
            {
                if (ores[i].startPoint < pathPoints.Length - 2)
                {
                    ores[i].currentLerp = 0;
                    ores[i].startPoint++;
                    ores[i].pathDistance = Vector3.Distance(pathPoints[ores[i].startPoint].position, pathPoints[ores[i].startPoint + 1].position);
                }
                else
                {
                    ores[i].isAtTheEnd = true;
                    onObjectTouchTheEnd?.Invoke(this);
                    continue;
                }
            }

            // Check if current item to close frome the front item
            if(i > 0 && Vector3.Distance(ores[i].ore.transform.position, ores[i - 1].ore.transform.position) <= itemSpacing)
            {
                continue;
            }

            // Move current item along the current line
            ores[i].ore.transform.position = Vector3.Lerp(pathPoints[ores[i].startPoint].position, pathPoints[ores[i].startPoint + 1].position, ores[i].currentLerp);
            ores[i].currentLerp += (moveSpeed * Time.deltaTime) / ores[i].pathDistance;
            
            // Check if there is place for a new item
            if (i == ores.Count - 1)
            {
                bool lastFrame = haveSpaceToAddItem;
                haveSpaceToAddItem = Vector3.Distance(ores[i].ore.transform.position, pathPoints[0].position) > itemSpacing;

                if (!lastFrame && haveSpaceToAddItem)
                {
                    onConveyorGetSpace?.Invoke(this);
                }
            }
        }
    }

    public void AddItem(Ore ore)
    {
        ores.Add(new ConveyorBeltOre(ore));
        haveSpaceToAddItem = false;
    }

    public Ore RemoveItem(ConveyorBeltOre conveyorOre)
    {
        Ore _ore = conveyorOre.ore;

        ores.Remove(conveyorOre);
        rsoOreManager.Value.DestroyOre(conveyorOre.ore);

        return _ore;
    }

    public ConveyorBeltOre GetFirstItem()
    {
        if(ores.Count > 0)
        {
            return ores[0];
        }
        else
        {
            return null;
        }
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