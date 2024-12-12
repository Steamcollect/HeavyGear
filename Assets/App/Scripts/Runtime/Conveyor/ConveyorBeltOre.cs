using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ConveyorBeltOre
{
    public Ore ore;

    [HideInInspector] public float currentLerp = 1;
    [FormerlySerializedAs("startPoint")] [HideInInspector] public int currentPoint = -1;

    [HideInInspector] public float pathDistance;
    [HideInInspector] public bool isAtTheEnd = false;

    public ConveyorBeltOre(Ore ore)
    {
        this.ore = ore;
    }

    public ConveyorBeltOre(Ore ore, int pathPoint)
    {
        this.ore = ore;
        this.currentPoint = pathPoint;
        currentLerp = 0;
    }
}