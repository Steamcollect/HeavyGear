using UnityEngine;

[System.Serializable]
public class ConveyorBeltOre
{
    public Ore ore;

    [HideInInspector] public float currentLerp = 1;
    [HideInInspector] public int startPoint = -1;

    [HideInInspector] public float pathDistance;
    [HideInInspector] public bool isAtTheEnd = false;

    public ConveyorBeltOre(Ore ore)
    {
        this.ore = ore;
    }
}