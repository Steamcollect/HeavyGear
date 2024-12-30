using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float itemSpacing;

    [HideInInspector] public bool haveSpaceToAddItem = true;

    [Header("References")]
    [SerializeField] private PathPoint[] pathPointsList;

    List<ConveyorBeltOre> ores = new List<ConveyorBeltOre>();

    public Action<ConveyorBelt> onObjectTouchTheEnd;
    public Action<ConveyorBelt> onConveyorGetSpace;

    [Space(10)]
    [SerializeField] RSO_OreManager rsoOreManager;

    private void Awake()
    {
        for (var index = 0; index < pathPointsList.Length; index++)
        { 
            if (index == 0) continue;
            Vector3 direction = (pathPointsList[index].transform.position - pathPointsList[index-1].transform.position).normalized;
            Vector3 pointThreashold = pathPointsList[index].transform.position - direction * itemSpacing;

            Vector3 AC = pointThreashold - pathPointsList[index-1].transform.position;
            Vector3 AB = pathPointsList[index].transform.position - pathPointsList[index-1].transform.position;
            
            // Debug.DrawRay(pathPointsList[index-1].transform.position, AB, Color.yellow,15f);
            // Debug.DrawRay(pathPointsList[index-1].transform.position, AC, Color.red,15f);
            pathPointsList[index].threasholdLerp = AC.magnitude/AB.magnitude;
        }
    }


    public void ChangePathPointAvailable(int indexPath, bool blocked)
    {
        pathPointsList[indexPath].blocked = blocked;
    }
    
    private void FixedUpdate()
    {
        MoveItems();
    }

    void MoveItems()
    {
        if(ores.Count <= 0) haveSpaceToAddItem = true;

        for (int i = 0; i < ores.Count; i++)
        {
            // If item reach the end of the line
            if (ores[i].currentLerp >= 1)
            {
                SwapItemToNextPoint(ref i);
                continue;
            }
            
            if(CheckCannotMoveItem(ref i))
            {
                continue;
            }
            
            MoveItem(ref i);
            
            CheckHaveSpaceToAddItem(ref i);
        }
    }

    
    /// <summary>
    /// Check if there is place for a new item
    /// </summary>
    /// <param name="i"></param>
    private void CheckHaveSpaceToAddItem(ref int i)
    {
        if (i == ores.Count - 1)
        {
            bool lastFrame = haveSpaceToAddItem;
            haveSpaceToAddItem = Vector3.Distance(ores[i].ore.transform.position, pathPointsList[0].transform.position) > itemSpacing;

            if (!lastFrame && haveSpaceToAddItem)
            {
                onConveyorGetSpace?.Invoke(this);
            }
        }
    }

    /// <summary>
    /// Move current item along the current line
    /// </summary>
    /// <param name="i"></param>
    private void MoveItem(ref int i)
    {
        ores[i].ore.transform.position = Vector3.Lerp(pathPointsList[ores[i].currentPoint].transform.position, pathPointsList[ores[i].currentPoint + 1].transform.position, ores[i].currentLerp);
        ores[i].currentLerp += (moveSpeed * Time.deltaTime) / ores[i].pathDistance;
    }

    /// <summary>
    /// Change the item to the next path point or the end of the conveoyor
    /// </summary>
    /// <param name="i"></param>
    private void SwapItemToNextPoint(ref int i)
    {
        if (ores[i].currentPoint < pathPointsList.Length - 2) //End of path point
        {
            ores[i].currentLerp = 0;
            ores[i].currentPoint++;
            ores[i].pathDistance = Vector3.Distance(pathPointsList[ores[i].currentPoint].transform.position, pathPointsList[ores[i].currentPoint + 1].transform.position);
        }
        else //Touch the end of the conveoyor
        {
            ores[i].isAtTheEnd = true;
            onObjectTouchTheEnd?.Invoke(this);
        }
    }

    /// <summary>
    /// Check if current item to close frome the front item
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private bool CheckCannotMoveItem(ref int i)
    {
        bool itemBlockedByNextPoint = ores[i].currentPoint + 1 <= pathPointsList.Length && pathPointsList[ores[i].currentPoint+1].blocked && Utils.NumberInRange(pathPointsList[ores[i].currentPoint + 1].threasholdLerp - ores[i].currentLerp,-0.03f,0.03f);
        
        bool itemCannotMove = i > 0 &&
                              Vector3.Distance(ores[i].ore.transform.position, ores[i - 1].ore.transform.position) <=
                              itemSpacing;
        return itemCannotMove  || itemBlockedByNextPoint;
    }

    public void AddItem(Ore ore)
    {
        ores.Add(new ConveyorBeltOre(ore));
        ore.transform.position = pathPointsList[0].transform.position;
        haveSpaceToAddItem = false;
    }

    public void AddItem(Ore ore, int indexPath)
    {
        var convOre = new ConveyorBeltOre(ore, indexPath);
        if (ores.Count > 0)
        {
            var index = ores.FindLastIndex(o => o.currentPoint == indexPath - 1);
            ores.Insert(index == -1 ? 0:index,convOre); 
        }
        else ores.Add(convOre);
        convOre.pathDistance = Vector3.Distance(pathPointsList[indexPath].transform.position, pathPointsList[indexPath + 1].transform.position);
        convOre.ore.transform.position = Vector3.Lerp(pathPointsList[indexPath].transform.position, pathPointsList[indexPath + 1].transform.position,0);
    }

    public Ore RemoveItem(ConveyorBeltOre conveyorOre)
    {
        Ore ore = conveyorOre.ore;

        ores.Remove(conveyorOre);
        rsoOreManager.Value.DestroyOre(conveyorOre.ore);

        return ore;
    }

    public ConveyorBeltOre GetFirstItem()
    {
        if(ores.Count > 0)
        {
            return ores[0];
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if(pathPointsList.Length > 0)
        {
            for (int i = 0; i < pathPointsList.Length - 1; i++)
            {
                Gizmos.DrawLine(pathPointsList[i].transform.position, pathPointsList[i + 1].transform.position);
            }
        }        
    }
}

[System.Serializable]
public class PathPoint
{
    public Transform transform;
    public bool blocked;
    public float threasholdLerp = -1f;
}